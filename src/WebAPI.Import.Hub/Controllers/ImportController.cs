using Microsoft.AspNetCore.Mvc;
using Core.Extensions;
using Core.Models;

namespace Services.Import.Hub.Controllers;

// TODO: Maybe refine this process, to reduce the amount of Queries needed
/// <summary>
/// Controller for Importing Data (Events and Persons) into the database
/// General aproach should be in the following order:
/// 1. (Optional) Clear the Database (2 Queries)
/// 2. Fetch all Rooms from the Database (1 Query)
/// 3. Insert all Persons into the Database (1 Query)
/// 4. With the fetched rooms insert the events (1 Query)
/// 5. Assign the Persons to the Events (1 Query)
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class ImportController(ILogger<ImportController> logger, IConfiguration config) : Controller
{
    protected readonly IConfiguration _config = config;
    protected readonly ILogger _logger = logger;
    protected HttpClient _httpClient = new();

    /// <summary>
    /// Endpoint to clear all Events
    /// </summary>
    /// <returns>Number of rows deleted</returns>
    [HttpGet("event/clear")]
    public async Task<ActionResult<int>> ClearEvents()
    {
        var eventChangesDeleted = await HttpExtensions.HttpDeleteSingleAsync<int>(_httpClient, _config["Services:EventChanges"]);
        var eventsDeleted = await HttpExtensions.HttpDeleteSingleAsync<int>(_httpClient, _config["Services:Events"]);

        _logger.LogDebug("Deleted {num} events from Database", eventsDeleted + eventChangesDeleted);

        return Ok(eventsDeleted + eventChangesDeleted);
    }

    /// <summary>
    /// Endpoint to delete all Persons
    /// </summary>
    /// <returns>Number of rows deleted</returns>
    [HttpGet("person/clear")]
    public async Task<ActionResult<int>> ClearPersons()
    {
        var personsDeleted = await HttpExtensions.HttpDeleteSingleAsync<int>(_httpClient, _config["Services:Persons"]);

        _logger.LogDebug("Deleted {num} persons from Database", personsDeleted);

        return Ok(personsDeleted);
    }

    /// <summary>
    /// Internal Only, Checks the Events in the Database for duplicates
    /// </summary>
    /// <param name="rooms">List of Rooms in the Database</param>
    /// <param name="events">List of Events in the Database</param>
    /// <param name="evt">The Event to check if it is a duplicate</param>
    /// <returns>The Event evt if no duplicate is found, null otherwise</returns>
    private Event? CheckEventDuplicates(ICollection<Room> rooms, ICollection<Event> events, Event evt)
    {
        // Maybe not needed?
        // var e = events.Find(e => e.FileId == evt.FileId);
        // if (e != null) return BadRequest("An Event with this FileId already exists");

        var r = rooms.ToList().Find(r => r.Id == evt.RoomId);
        if (r == null)
        {
            _logger.LogWarning("The provided RoomId does not match to an existing room");
            return null;
        }

        var e = events.ToList().Find(e =>
            e.TimestampFrom == evt.TimestampFrom &&
            (e.TimestampTo != null && (e.TimestampTo == evt.TimestampTo)) &&
            e.RoomId != evt.RoomId);
        if (e != null)
        {
            _logger.LogWarning("An Event in the same Room for the same Time already exists");
            return null;
        }

        return evt;
    }

    /// <summary>
    /// Endpoint for Importing an Event into the Database
    /// </summary>
    /// <param name="evt">The Event to import</param>
    /// <returns>The Event Entity</returns>
    [HttpPost("event")]
    public async Task<ActionResult<Event>> ImportEvent(Event evt)
    {
        var events = await HttpExtensions.HttpGetMultipleAsync<Event>(_httpClient, _config["Services:Events"]);
        var rooms = await HttpExtensions.HttpGetMultipleAsync<Room>(_httpClient, _config["Services:Rooms"]);

        var e = CheckEventDuplicates(rooms, events, evt);
        if (e == null) return BadRequest("There is a problem with the passed event-object");

        _logger.LogDebug("Try to import {event}", e);

        e = await HttpExtensions.HttpPostSingleAsync<Event>(_httpClient, _config["Services:Events"], e);

        if (e == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        _logger.LogDebug("Event imported eith Id: {id}", e.Id);

        return Ok(e);
    }

    /// <summary>
    /// Endpoint for Importing multiple Events at once
    /// </summary>
    /// <param name="evts">A List of events</param>
    /// <returns>The List of Events imported</returns>
    [HttpPost("events")]
    public async Task<ActionResult<int>> ImportEvents(List<Event> evts)
    {
        var events = await HttpExtensions.HttpGetMultipleAsync<Event>(_httpClient, _config["Services:Events"]);
        var rooms = await HttpExtensions.HttpGetMultipleAsync<Room>(_httpClient, _config["Services:Rooms"]);

        ICollection<Event> result = [];

        foreach (var evt in evts)
        {
            var e = CheckEventDuplicates(rooms, events, evt);
            if (e == null) continue;

            _ = result.Append(e);
        }

        _logger.LogDebug("Try to import {events}", result);

        var resultValue = await HttpExtensions.HttpPostSingleAsync<int>(_httpClient, $"{_config["Services:Events"]}/range", result);

        _logger.LogDebug("Imported {num} Events successfully", resultValue);

        return Ok(resultValue);
    }

    /// <summary>
    /// Internal only, Checks the Persons in the Database for duplicates
    /// </summary>
    /// <param name="personDb">The List of Persons in the Database</param>
    /// <param name="person">The Person to check for</param>
    /// <returns>The Person Entity if no duplicate was found, null otherwise</returns>
    private Person? CheckPersonDuplicates(ICollection<Person> personDb, Person person)
    {
        var p = personDb.ToList().Find(p =>
            p.Description == person.Description ||
            p.Id == person.Id);
        if (p != null)
        {
            _logger.LogWarning("Person already exists");
            return null;
        }

        return person;
    }

    /// <summary>
    /// Endpoint for importing a Person
    /// </summary>
    /// <param name="person">The Person to import</param>
    /// <returns>The Person Entity</returns>
    [HttpPost("person")]
    public async Task<ActionResult<Person>> ImportPerson(Person person)
    {
        var persons = await HttpExtensions.HttpGetMultipleAsync<Person>(_httpClient, _config["Services:Persons"]);

        var p = CheckPersonDuplicates(persons, person);
        if (p == null) return BadRequest("There is a problem with the passed person-object");

        _logger.LogDebug("Try to import {person} into database", p);

        p = await HttpExtensions.HttpPostSingleAsync<Person>(_httpClient, _config["Services:Persons"], p);
        if (p == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        _logger.LogDebug("Person imported eith Id: {id}", p.Id);

        return Ok(p);
    }

    /// <summary>
    /// Endpoint for importing multiple Persons into the Database
    /// </summary>
    /// <param name="persons">A List of Persons</param>
    /// <returns>A List of Person Entities</returns>
    [HttpPost("persons")]
    public async Task<ActionResult<int>> ImportPersons(ICollection<Person> persons)
    {
        var personsDb = await HttpExtensions.HttpGetMultipleAsync<Person>(_httpClient, _config["Services:Persons"]);

        ICollection<Person> result = [];

        foreach (var person in persons)
        {
            var p = CheckPersonDuplicates(personsDb, person);
            if (p == null)
            {
                _logger.LogWarning("Duplicate found of person {id}", person.Id);
                continue;
            }

            result.Add(p);
        }

        _logger.LogDebug("Try to import {persons} ", result);

        var resultValue = await HttpExtensions.HttpPostSingleAsync<int>(_httpClient, $"{_config["Services:Persons"]}/range", result);

        _logger.LogDebug("Imported {num} Persons successfully", resultValue);

        return Ok(resultValue);
    }

    /// <summary>
    /// Endpoint that assigns 1 or more Persons to a event, multiple events can be assigned at once
    /// </summary>
    /// <param name="data">Dictionary with the Event Id as Key and the Ids of the Persons as Array</param>
    /// <returns>A List of the Changed Events</returns>
    /*[HttpGet("assigntoevent")]
    public async Task<ActionResult<ICollection<Event>>> AssignPersonToEvent([FromQuery] IDictionary<Guid, ICollection<Guid>> data)
    {
        var evts = (await _workService._eventService.GetEntites()).ToList();
        var persons = (await _workService._personService.GetEntites()).ToList();

        ICollection<Event> result = new List<Event>();

        foreach(var evtId in data.Keys)
        {
            var e = evts.Find(e => e.Id == evtId);
            if (e == null)
            {
                _logger.LogWarning("Could not find Event-Id {id}", evtId);
                continue;
            }

            foreach(var pId in data[evtId])
            {
                var p = persons.Find(p => p.Id == pId);
                if (p == null)
                {
                    _logger.LogWarning("Could not find Person-Id {id}", pId);
                    continue;
                }
                e.Persons.Add(p);
            }

            e = await _workService._eventService.UpdateEntity(evtId, e);
            if (e == null)
            {
                _logger.LogError("Could not update Event {id}", evtId);
                continue;
            }

            result.Add(e);
        }


        return Ok(result);
    }*/

    /// <summary>
    /// Returns all Rooms in the database.
    /// This is for passthrough to the Importer only.
    /// </summary>
    /// <returns>A List of Room Entities</returns>
    [HttpGet("rooms")]
    public async Task<ActionResult<ICollection<Room>>> GetRooms()
    {
        var rooms = await HttpExtensions.HttpGetMultipleAsync<Room>(_httpClient, _config["Services:Rooms"]);

        return Ok(rooms);
    }

    /// <summary>
    /// Returns all Departments in the database.
    /// This is for passthrough to the Importer only.
    /// </summary>
    /// <returns>A List of Department Entities</returns>
    [HttpGet("departments")]
    public async Task<ActionResult<ICollection<Department>>> GetDeparmtents()
    {
        var departments = await HttpExtensions.HttpGetMultipleAsync<Department>(_httpClient, _config["Services:Departments"]);

        return Ok(departments);
    }
}
