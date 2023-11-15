// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Infrastructure.Models.Settings;

[Table("Permissions")]
public class Permission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Ressource { get; set; }

    [Required]
    public string Member { get; set; }

    [Required]
    public bool GET { get; set; }

    [Required]
    public bool PUT { get; set; }

    [Required]
    public bool POST { get; set; }

    [Required]
    public bool DELETE { get; set; }
}