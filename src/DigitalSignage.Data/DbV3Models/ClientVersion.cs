// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Data.DbV3Models;

[Table("ClientVersions")]
[PrimaryKey(nameof(Id))]
public class ClientVersion<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string Version { get; set; } = "";

    public byte[] Data { get; set; } = [];

    [MaxLength(512)]
    public string Path { get; set; } = "";
}
