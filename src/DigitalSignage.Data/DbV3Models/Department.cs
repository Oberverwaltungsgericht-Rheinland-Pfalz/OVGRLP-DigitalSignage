// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("Departments")]
[PrimaryKey(nameof(Id))]
public class Department<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = "";

    [JsonIgnore]
    public ICollection<Event<T>> Events { get; set; } = new List<Event<T>>();
    [JsonIgnore]
    public ICollection<EventChange<T>> EventChanges { get; set; } = new List<EventChange<T>>();
}
