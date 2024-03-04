namespace Core.Models.Json;

public enum ScheduleAction
{
    DeleteEvents = 0,
    DeletePersons = 1,
}

/// <summary>
/// Schedule Data with Actions to perform
/// </summary>
/// <typeparam name="T"></typeparam>
public class ScheduleDataJson
{
    public Guid ScheduleId { get; set; }

    // TODO: Change to Enum
    // Action to perform
    public string Action { get; set; } = "";

    // Targets that are related to the Action
    public List<Guid> Targets { get; set; } = new();
}
