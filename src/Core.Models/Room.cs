using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

[Table("Rooms")]
[PrimaryKey(nameof(Id))]
[Index(nameof(RoomNumber), IsUnique = true)]
public class Room : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Room (e.g. Sitzungssaal)
    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = "";

    // RoomNumber (e.g. A021)
    [Required]
    [MaxLength(32)]
    public string RoomNumber { get; set; } = "";

    // Events with this Room assigned
    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = [];

    [JsonIgnore]
    public ICollection<EventChange> EventChanges { get; set; } = [];

    // Displays with this Room assigned
    [JsonIgnore]
    public ICollection<Display> Displays { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}\n" +
            $"\tNumber:\t\t{RoomNumber}";
    }
}
