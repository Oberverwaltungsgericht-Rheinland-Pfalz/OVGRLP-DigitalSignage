// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Infrastructure.Models.Settings;

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
    public ICollection<NoteAssignment> NotesAssignments { get; set; }
    public bool Dummy { get; set; }
}