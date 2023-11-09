// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DigitalSignage.Infrastructure.Models.Settings
{
  [Table("NoteAssignments")]
  public class NoteAssignment
  {
    [Key]
    public int Id { get; set; }

    public virtual Note Note { get; set; }

    public int NoteId { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public string Comment { get; set; }

    public int DisplayId { get; set; }

    public virtual Display Display { get; set; }
  }
}