// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.JsonData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("Filters")]
[PrimaryKey(nameof(Id))]
[Microsoft.EntityFrameworkCore.Index(nameof(Name), IsUnique = true)]
public class Filter<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    // Unique
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    public List<FilterDataJson<T>> Data { get; set; } = new();

    // NOTE: These are just for Backtracking not for adding Groups (etc.) to a Filter...
    [JsonIgnore]
    public ICollection<Group<T>> Groups { get; set; } = new List<Group<T>>();
    [JsonIgnore]
    public ICollection<Display<T>> Displays { get; set; } = new List<Display<T>>();
    [JsonIgnore]
    public ICollection<Notification<T>> Notifications { get; set; } = new List<Notification<T>>();

}
