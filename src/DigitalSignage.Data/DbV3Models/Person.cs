// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

public enum PersonType
{
    Plaintiff = 0,
    Defendant = 1,
    Representative = 2,
    Attorney = 3,
    Witness = 4,
    ExpertWitness = 5,
    CoSummonend = 6,
}


[Table("Persons")]
[PrimaryKey(nameof(Id))]
public class Person<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public PersonType Type { get; set; }

    [JsonIgnore]
    public ICollection<Event<T>> Events { get; set; } = new List<Event<T>>();
    [JsonIgnore]
    public ICollection<EventChange<T>> EventChanges { get; set; } = new List<EventChange<T>>();
}
