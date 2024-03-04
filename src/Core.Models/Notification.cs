using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Notification : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Notification
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // Starttime when the Notification should be shown
    [Required]
    public DateTime TimestampFrom { get; set; }

    // Endtime when the Notification should not be shown anymore
    public DateTime? TimestampTo { get; set; }

    // Filter to apply on this Notification
    [JsonIgnore]
    public Guid? FilterId { get; set; }
    [JsonIgnore]
    public Filter? Filter { get; set; }

    // Template to apply on this Notification
    [JsonIgnore]
    public Guid? TemplateId { get; set; }
    [JsonIgnore]
    public Template? Template { get; set; }

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}\n" +
            $"\tFrom:\t\t{TimestampFrom}\n" +
            $"\tTo:\t\t{TimestampTo}\n" +
            $"\tFilter:\t\t{FilterId}\n" +
            $"\tTemplate:\t{TemplateId}";
    }
}
