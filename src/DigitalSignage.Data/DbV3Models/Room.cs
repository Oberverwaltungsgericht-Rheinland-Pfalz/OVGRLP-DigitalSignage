// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("Rooms")]
[PrimaryKey(nameof(Id))]
[Microsoft.EntityFrameworkCore.Index(nameof(RoomNumber), IsUnique = true)]
public class Room<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(32)]
    public string RoomNumber { get; set; } = "";

    [JsonIgnore]
    public ICollection<Event<T>> Events { get; set; } = new List<Event<T>>();

    [JsonIgnore]
    public ICollection<EventChange<T>> EventChanges { get; set; } = new List<EventChange<T>>();

    [JsonIgnore]
    public ICollection<Display<T>> Displays { get; set; } = new List<Display<T>>();
}
