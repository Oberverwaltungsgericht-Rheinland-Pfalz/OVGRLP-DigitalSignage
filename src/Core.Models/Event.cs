using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public enum EventStatus
{
    Upcoming = 0,
    Running = 1,
    Cancelled = 2,
    Done = 3
}

public enum EventType
{
    Hearing = 0,    // mündl. Verhandlung
    NoHearing = 1,  // ohne mündl. Verhandlung
}

[Table("Events")]
[PrimaryKey(nameof(Id))]
public class Event : IBaseModel
{
    // Entity-Id
    public Guid Id { get; set; }

    // Sorting-Order
    public uint Order { get; set; }

    // Chamber of Court
    [Required]
    public uint Chamber { get; set; }

    // Time when the Event starts
    [Required]
    public DateTime TimestampFrom { get; set; }
    // Time when the Event ends
    public DateTime? TimestampTo { get; set; }

    // Is the Event public?
    [Required]
    public bool Public { get; set; }

    // FileId (Aktenzeichen)
    [Required]
    public string FileId { get; set; } = "";

    // Type of Event
    [Required]
    public EventType Type { get; set; } = EventType.Hearing;

    // Like a Description
    [Required]
    public string Subject { get; set; } = "";

    // Additional Information
    public string Comments { get; set; } = "";

    // Eventstatus (like upcoming, ongoing, done...)
    public EventStatus Status { get; set; } = EventStatus.Upcoming;

    // Assigned Department
    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }

    // Assigned Room where the Event is going to be held in
    public Guid? RoomId { get; set; }
    public Room? Room { get; set; }

    // Assigned EventChanges that have been edited
    public Guid? EventChangeId { get; set; }
    public EventChange? EventChange { get; set; }

    // Link to Persons that are assigned to the Event
    // [JsonIgnore]
    public ICollection<Person> Persons { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tOrder:\t\t{Order}\n" +
            $"\tChamber:\t{Chamber}\n" +
            $"\tFrom:\t\t{TimestampFrom}\n" +
            $"\tTo:\t\t{TimestampTo}\n" +
            $"\tFileId:\t\t{FileId}\n" +
            $"\tType:\t\t{Type}\n" +
            $"\tSubject:\t{Subject}\n" +
            $"\tComments:\t{Comments}\n" +
            $"\tStatus:\t\t{Status}\n" + 
            $"\tDepartment:\t{DepartmentId}\n" +
            $"\tRoom:\t\t{RoomId}\n" +
            $"\tChanges:\t\t{EventChangeId}";
    }
}
