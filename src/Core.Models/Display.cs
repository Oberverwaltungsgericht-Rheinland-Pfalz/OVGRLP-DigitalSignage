using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Core.Models;

public enum DisplayStatus
{
    Created = 0,
    Registered = 1,
    Online = 2,
    Offline = 3,
    Disabled = 4
}

[Table("Displays")]
[PrimaryKey(nameof(Id))]
public class Display : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Name of the Display
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    // IP of the Display (For access only)
    [NotMapped]
    public string IpStr
    {
        get  
        {
            return Ip.ToString();
        }
        set
        {
            Ip = IPAddress.Parse(value);
        }
    }

    // Mac of the Display (For access only)
    [NotMapped]
    public string MacStr
    {
        get
        {
            return Mac.ToString();
        }
        set
        {
            Mac = PhysicalAddress.Parse(value);
        }
    }

    // Mac (Internal only)
    [Required]
    [JsonIgnore]
    public PhysicalAddress Mac { get; set; } = PhysicalAddress.Parse("00:00:00:00:00:00");

    // Ip (Internal only)
    [Required]
    [JsonIgnore]
    public IPAddress Ip { get; set; } = IPAddress.Parse("0.0.0.0");

    // (Reserved) PublicKey of the Display, not used yet
    public string PublicKey { get; set; } = "";

    // Is this a Dummy-Display?
    public bool Dummy { get; set; } = false;

    // Status of the Display (Shutdown, online,...)
    public DisplayStatus Status { get; set; } = DisplayStatus.Created;

    // Template for the Display
    public Guid? TemplateId { get; set; }
    [JsonIgnore]
    public Template? Template { get; set; }

    // Group for the Display
    public Guid? GroupId { get; set; }
    [JsonIgnore]
    public Group? Group { get; set; }

    // Filter for the Display
    public Guid? FilterId { get; set; }
    [JsonIgnore]
    public Filter? Filter { get; set; }

    // Room for the Display
    public Guid? RoomId { get; set; }
    [JsonIgnore]
    public Room? Room { get; set; }

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tId:\t\t{Id}\n" +
            $"\tName:\t\t{Name}\n" +
            $"\tIPAddr:\t\t{IpStr}\n" +
            $"\tMacAddr:\t{MacStr}\n" +
            $"\tPublicKey:\t{PublicKey}\n" +
            $"\tDummy:\t\t{Dummy}\n" +
            $"\tTemplate:\t{TemplateId}\n" +
            $"\tGroup:\t\t{GroupId}\n" +
            $"\tFilter:\t\t{FilterId}\n" +
            $"\tRoom:\t\t{RoomId}";
    }
}
