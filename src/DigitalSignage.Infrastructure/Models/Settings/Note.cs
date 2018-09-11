using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Infrastructure.Models.Settings
{
  [Table("Notes")]
  public class Note
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [MaxLength]
    public string Content { get; set; }

    [Required]
    public bool Forced { get; set; }

    public ICollection<NoteAssignment> NotesAssignments { get; set; }
  }
}