// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("Groups")]
[PrimaryKey(nameof(Id))]
public class Group<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    public bool Hidden { get; set; }

    [JsonIgnore]
    public Guid? TemplateId { get; set; }
    [JsonIgnore]
    public Template<T>? Template { get; set; }

    [JsonIgnore]
    public Guid? FilterId { get; set; }
    [JsonIgnore]
    public Filter<T>? Filter { get; set; }

    [JsonIgnore]
    public ICollection<Display<T>> Displays { get; set; } = new List<Display<T>>();
}
