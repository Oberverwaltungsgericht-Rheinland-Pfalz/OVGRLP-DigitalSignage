using Microsoft.EntityFrameworkCore;
using Core.Models.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

[Table("Templates")]
[PrimaryKey(nameof(Id))]
public class Template : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Template
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // Html to use for this Template
    public List<TemplateHtmlJson> Html { get; set; } = [];

    // Css for this Template
    public List<TemplateCssJson> Css { get; set; } = [];

    // Displays that have this Template assigned
    [JsonIgnore]
    public ICollection<Display> Displays { get; set; } = [];
    // Notifications that have this Template assigned
    [JsonIgnore]
    public ICollection<Notification> Notifications { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}";
    }
}
