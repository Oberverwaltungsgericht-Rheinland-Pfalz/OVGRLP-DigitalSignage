using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Infrastructure.Models.Settings
{
  [Table("Displays")]
  public class Display
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Template { get; set; }
    public string Styles { get; set; }
    public string Filter { get; set; }
    public string Group { get; set; }
    public string ControlUrl { get; set; }
    public string NetAddress { get; set; }
    public string WolIpAddress { get; set; }
    public string WolMacAddress { get; set; }
    public int WolUdpPort { get; set; }
    public string Description { get; set; }
    public ICollection<Note> Notes { get; set; }
    public bool Dummy { get; set; }
  }
}