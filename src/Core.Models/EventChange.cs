using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

/// <summary>
/// See Event for more Information
/// </summary>
/// <typeparam name="T"></typeparam>
[Table("EventChanges")]
[PrimaryKey(nameof(Id))]
public class EventChange : IBaseModel
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

    public Guid? EventId { get; set; }
    public Event? Event { get; set; }

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
            $"\tEvent:\t\t{EventId}";
    }
}
