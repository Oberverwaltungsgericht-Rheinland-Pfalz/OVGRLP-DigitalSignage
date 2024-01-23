// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("EventChanges")]
[PrimaryKey(nameof(Id))]
public class EventChange<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    public uint Order { get; set; }

    [Required]
    public uint Chamber {  get; set; }

    [Required]
    public DateTime TimestampFrom { get; set; }
    [Required]
    public DateTime TimestampTo { get; set; }

    [Required]
    public bool Public { get; set; }

    [Required]
    public string FileId { get; set; } = "";

    [Required]
    public string Type { get; set; } = "";

    [Required]
    public string Subject { get; set; } = "";

    public EventStatus Status { get; set; } = EventStatus.Upcoming;

    public Guid? DepartmentId { get; set; }
    public Department<T>? Department { get; set; }

    public Guid? RoomId { get; set; }
    public Room<T>? Room { get; set; }

    public Guid? EventId { get; set; }
    public Event<T>? Event { get; set; }

    [JsonIgnore]
    public ICollection<Person<T>> Persons { get; set; } = new List<Person<T>>();
}
