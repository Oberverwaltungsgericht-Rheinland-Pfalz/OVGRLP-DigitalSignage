using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

[Table("Departments")]
[PrimaryKey(nameof(Id))]
public class Department : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Department
    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = "";

    // Events that have been assigned to the Department
    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = [];
    [JsonIgnore]
    public ICollection<EventChange> EventChanges { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}\n";
    }
}
