using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

[Table("Groups")]
[PrimaryKey(nameof(Id))]
public class Group : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Group
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // Is the Group hidden?
    public bool Hidden { get; set; }

    // Template for a whole Group
    [JsonIgnore]
    public Guid? TemplateId { get; set; }
    [JsonIgnore]
    public Template? Template { get; set; }

    // Filter for a whole Group
    [JsonIgnore]
    public Guid? FilterId { get; set; }
    [JsonIgnore]
    public Filter? Filter { get; set; }

    // Displays that are in the Group
    [JsonIgnore]
    public ICollection<Display> Displays { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}\n" +
            $"\tHidden:\t\t{Hidden}\n" +
            $"\tTemplate:\t{TemplateId}\n" +
            $"\tFilter:\t\t{FilterId}\n" +
            $"\tDisplays:\t{Displays}";
    }
}
