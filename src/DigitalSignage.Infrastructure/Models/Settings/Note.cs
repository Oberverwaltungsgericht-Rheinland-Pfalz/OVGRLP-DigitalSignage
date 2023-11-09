// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
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