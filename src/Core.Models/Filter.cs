using Microsoft.EntityFrameworkCore;
using Core.Models.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

[Table("Filters")]
[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = true)]
public class Filter : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Unique, Name of the Filter, for better Oversight
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // List of FilterData that will be applied
    public List<FilterDataJson> Data { get; set; } = [];

    // NOTE: These are just for Backtracking not for adding Groups (etc.) to a Filter...
    // So you can see where a Filter is used ...
    [JsonIgnore]
    public ICollection<Group> Groups { get; set; } = [];
    [JsonIgnore]
    public ICollection<Display> Displays { get; set; } = [];
    [JsonIgnore]
    public ICollection<Notification> Notifications { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}";
    }
}
