using CLI.Import.XML.Models;
using CLI.Import.XML.Services;
using Microsoft.IdentityModel.Tokens;
using Core.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CLI.Import.XML;

/// <summary>
/// Clas that implements all CLI-Actions
/// </summary>
public class CLIActions
{
    public List<string> InputFiles;

    public bool ClearDatabase = false;
    public bool TestRun = false;
    public bool ShowInformation = false;
    public bool ShowHelp = false;
    public bool Verbose = false;

    private readonly HttpClient _httpClient;
    private readonly string _host;


    public CLIActions(string Host)
    {
        InputFiles = [];
        _httpClient = new HttpClient();
        _host = Host;

        if (TestRun)
            Console.WriteLine($"Using Host: {Host}");
    }

    /// <summary>
    /// Checks for all needed Parameters to be Set
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void ValidateActions()
    {
        if (ShowInformation || ShowHelp)
            return;

        if (!ClearDatabase && InputFiles.IsNullOrEmpty())
            throw new ArgumentException("Es wurde keine XML-Datei angegeben!");
    }

    /// <summary>
    /// Executes the passed Actions
    /// </summary>
    /// <param name="cliService"></param>
    public void ExecuteActions(CLIService cliService)
    {
        if (ShowHelp)
        {
            ShowHelpAction(cliService);
            return;
        }

        if (ShowInformation)
        {
            ShowInformationAction();
            return;
        }

        List<Terminsaushang> termine = [];

        if (ClearDatabase && !TestRun)
        {
            ClearDatabaseAction();
        }

        foreach (string inputFile in InputFiles)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                Console.WriteLine("Empty String as Fileinput. Skipping...");
                continue;
            }

            try
            {
                var filePath = Path.GetFullPath(inputFile);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($" File {inputFile} could not be found");
                    continue;
                }

                Terminsaushang? data = XMLService.DeserializeFromXml<Terminsaushang>(filePath);
                if (data == null)
                {
                    Console.WriteLine("Could not load Data from the given XML-File");
                    continue;
                }

                termine.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        InsertEvents(termine);
    }

    /// <summary>
    /// Shows Information in the cli when using the Help-Argument
    /// </summary>
    /// <param name="cliService"></param>
    public static void ShowHelpAction(CLIService cliService)
    {
        Console.WriteLine("Usage: CLI.Import.XML.exe [OPTIONS]+ [PARAMETERS]");
        Console.WriteLine();
        Console.WriteLine("OPTIONS:");
        cliService.Options.WriteOptionDescriptions(Console.Out);
    }

    /// <summary>
    /// Shows the Version of the CLI
    /// </summary>
    public void ShowInformationAction()
    {
        Console.WriteLine($"CLI.Import.XML Version {GetType().Assembly.GetName().Version}");
    }

    /// <summary>
    /// Removes all Data from the Database
    /// </summary>
    public void ClearDatabaseAction()
    {
        var responseTask = _httpClient.GetAsync($"{_host}/event/clear");
        responseTask.Wait();
        if (responseTask.Result.IsSuccessStatusCode)
            Console.WriteLine($"Deleted {responseTask.Result.Content} events");

        responseTask = _httpClient.GetAsync($"{_host}/person/clear");
        responseTask.Wait();
        if (responseTask.Result.IsSuccessStatusCode)
            Console.WriteLine($"Deleted {responseTask.Result.Content} persons");
    }

    /// <summary>
    /// Inserts Events into the Database
    /// </summary>
    /// <param name="events">The List of Events to add</param>
    public void InsertEvents(List<Terminsaushang> events)
    {
        List<Room> rooms = GetRooms();
        List<Department> departments = GetDepartments();

        foreach (var inventory in events)
        {
            DateOnly importDate = DateOnly.Parse(inventory.Stammdaten.Datum);
            Department? department = departments.Find(d => d.Name == inventory.Stammdaten.Gerichtsname);
            if (department == null)
            {
                Console.WriteLine($"Department {inventory.Stammdaten.Gerichtsname} was not found");
                continue;
            }

            if (TestRun || Verbose)
                Console.WriteLine($"Department {department.Name} successfully assigned");

            foreach (var e in inventory.Terminiert)
            {
                InsertEvent(rooms, department, importDate, e);
            }
        }
       
    }

    /// <summary>
    /// Inserts a Single Event into the Database
    /// </summary>
    /// <param name="rooms">The Rooms from the Database</param>
    /// <param name="department">The Departments from the Database</param>
    /// <param name="importDate">The Date from the import</param>
    /// <param name="e">The Event XML-Object</param>
    public void InsertEvent(List<Room> rooms, Department department, DateOnly importDate, TerminsaushangVerfahren e)
    {
        Dictionary<PersonType, List<string>> persons = GetPersonsFromXml(e);
        TimeOnly eventTime = TimeOnly.Parse(e.Uhrzeit);

        if (TestRun)
            Console.WriteLine("EventTime: " + eventTime.ToString());

        Room? room = rooms.Find(r => r.Name.Contains(e.Sitzungssaal) || (r.Name + " " + r.RoomNumber).Contains(e.Sitzungssaal));
        if (room == null)
        {
            // TODO: Logging
            Console.WriteLine($"Room {e.Sitzungssaal} was not found");
            return;
        }

        if (TestRun)
            Console.WriteLine($"Found Room with Id {room.Id}");

        List<Person> personEntities = [];
        foreach (var person in persons.Keys)
        {
            foreach (var p in persons[person])
            {
                personEntities.Add(new Person
                {
                    Description = p,
                    Type = person
                });
            }
        }

        if (TestRun || Verbose)
            Console.WriteLine($"Persons to Insert: {personEntities.Count}");

        Event evt = new()
        {
            Order = Convert.ToUInt32(e.Lfdnr),
            Chamber = Convert.ToUInt32(e.Kammer),
            TimestampFrom = new DateTime(importDate, eventTime),
            Public = e.Oeffentlich == "ja",
            FileId = e.Az,
            // TODO: Convert String to Enum
            // Type = (EventType) e.Art,
            Subject = e.Gegenstand,
            Comments = e.Bemerkung1 + "\n" + e.Bemerkung2,
            RoomId = room.Id,
            DepartmentId = department.Id,
            Persons = personEntities
        };

        if (TestRun || Verbose)
        {
            Console.WriteLine("Insert Event: ");
            Console.WriteLine(evt.Subject);
            Console.WriteLine(evt.FileId);
        }

        if (!TestRun)
        {
            var evtTask = InsertEventTask(evt);
            evtTask.Wait();

            Event? result = evtTask.Result;
            if (result == null)
            {
                Console.WriteLine($"Could not insert Event into database");
            }

            Console.WriteLine($"Event inserted successfully with id {result?.Id}");
        }
    }

    /// <summary>
    /// Creates a List of Persons to Add, that are attending the Event
    /// </summary>
    /// <param name="persons">The List of Persons</param>
    /// <param name="type">The Type of Person</param>
    /// <param name="list">The List of Persons</param>
    private static void AddPersonList(ref Dictionary<PersonType, List<string>> persons, PersonType type, string[] list)
    {
        if (list.Length > 0)
            if (persons.TryGetValue(type, out List<string>? value))
                value.AddRange([.. list]);
            else
                persons.Add(type, [.. list]);
    }

    /// <summary>
    /// Function to execute the Event insertion via REST
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    private async Task<Event?> InsertEventTask(Event e)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(e),
            Encoding.UTF8, 
            "application/json");
        var response = await _httpClient.PostAsync($"{_host}/event", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        // TODO: Logging
        return null;
    }

    /// <summary>
    /// Loads all the available Rooms from the Database
    /// </summary>
    /// <returns>A List of Rooms</returns>
    private List<Room> GetRooms()
    {
        var responseTask = _httpClient.GetAsync($"{_host}/rooms");
        responseTask.Wait();
        if (!responseTask.Result.IsSuccessStatusCode)
        {
            Console.WriteLine("Could not load Rooms");
            Console.WriteLine($"Errorcode is: {responseTask.Result.StatusCode}");
            Console.WriteLine($"Message is: {responseTask.Result.ReasonPhrase}");
            return [];
        }

        var roomTask = responseTask.Result.Content.ReadFromJsonAsync<List<Room>>(); ;
        roomTask.Wait();
        List<Room> rooms = roomTask.Result ?? [];

        return rooms;
    }

    /// <summary>
    /// Loads all the available Departments from the Database
    /// </summary>
    /// <returns>A List of Departments</returns>
    private List<Department> GetDepartments()
    {
        var responseTask = _httpClient.GetAsync($"{_host}/departments");
        responseTask.Wait();
        if (!responseTask.Result.IsSuccessStatusCode)
        {
            Console.WriteLine("Could not load Departments");
            Console.WriteLine($"Errorcode is: {responseTask.Result.StatusCode}");
            Console.WriteLine($"Message is: {responseTask.Result.ReasonPhrase}");
            return [];
        }

        var departmentTask = responseTask.Result.Content.ReadFromJsonAsync<List<Department>>();
        departmentTask.Wait();
        List<Department> departments = departmentTask.Result ?? [];

        return departments;
    }

    /// <summary>
    /// Organizes Persons into a Dictionary with the Person-Type as Key
    /// </summary>
    /// <param name="e">The Event XML-Object</param>
    /// <returns>A Dictionary with all Structured Persons</returns>
    private Dictionary<PersonType, List<string>> GetPersonsFromXml(TerminsaushangVerfahren e)
    {
        Dictionary<PersonType, List<string>> persons = [];

        AddPersonList(ref persons, PersonType.Plaintiff, e.AktivPartei.Parteien);
        AddPersonList(ref persons, PersonType.PlaintiffAttorney, e.AktivPartei.ProzBev.PB);

        AddPersonList(ref persons, PersonType.Defendant, e.PassivPartei.Parteien);
        AddPersonList(ref persons, PersonType.DefendantAttourney, e.PassivPartei.ProzBev.PB);

        AddPersonList(ref persons, PersonType.Witness, e.Zeugen.Parteien);

        AddPersonList(ref persons, PersonType.CoSummonend, e.Beigeladen.Parteien);
        AddPersonList(ref persons, PersonType.CoSummonendAttourney, e.Beigeladen.ProzBev);

        AddPersonList(ref persons, PersonType.ExpertWitness, e.SV.Parteien);

        AddPersonList(ref persons, PersonType.Judge, e.Besetzung);

        AddPersonList(ref persons, PersonType.Translator, e.Dolmetscher.Parteien);

        if (TestRun || Verbose)
            foreach (var type in persons.Keys)
                foreach (var person in persons[type])
                    Console.WriteLine($"Found Person: {person}");

        return persons;
    }
}
