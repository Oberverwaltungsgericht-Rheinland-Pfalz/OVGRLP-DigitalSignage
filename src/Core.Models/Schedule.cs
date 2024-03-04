using Microsoft.EntityFrameworkCore;
using Core.Models.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

[Table("Schedules")]
[PrimaryKey(nameof(Id))]
public class Schedule : IBaseModel
{
    // Entity id
    public Guid Id { get; set; }

    // Name of the Schedule
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // Time when to Trigger this Schedule
    [Required]
    public DateTime TriggerOn { get; set; }

    // List of Actions to perform...
    public List<ScheduleDataJson> Data { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Name}\n" +
            $"\tTrigger:\t\t{TriggerOn}";
    }
}
