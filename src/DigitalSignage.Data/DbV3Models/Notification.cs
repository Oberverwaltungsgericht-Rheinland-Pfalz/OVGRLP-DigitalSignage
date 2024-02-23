// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

public class Notification<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    [Required]
    public DateTime TimestampFrom { get; set; }

    public DateTime? TimestampTo { get; set; }

    [JsonIgnore]
    public Guid? FilterId { get; set; }
    [JsonIgnore]
    public Filter<T>? Filter { get; set; }

    [JsonIgnore]
    public Guid? TemplateId { get; set; }
    [JsonIgnore]
    public Template<T>? Template { get; set; }
}
