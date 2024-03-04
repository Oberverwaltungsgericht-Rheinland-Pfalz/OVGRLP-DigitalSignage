using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;


[Table("ClientVersions")]
[PrimaryKey(nameof(Id))]
public class ClientVersion : IBaseModel
{
    // Entity Id
    public Guid Id { get; set; }

    // Version-String
    [Required]
    [MaxLength(32)]
    public string Version { get; set; } = "";

    // Byte-Data of the new Executable
    public byte[] Data { get; set; } = [];

    // OS-Type
    public PlatformID PlatformID { get; set; } = PlatformID.Win32NT;

    // Alternative for Byte-Data, Path where the Executable is stored
    [MaxLength(512)]
    public string Path { get; set; } = "";

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tVersion:\t{Version}\n" +
            $"\tData:\t\t{Data}\n" +
            $"\tPlatformID:\t{PlatformID}\n" +
            $"\tPath:\t\t{Path}";
    }
}
