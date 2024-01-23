// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.JsonData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignage.Data.DbV3Models;

[Table("Schedules")]
[PrimaryKey(nameof(Id))]
public class Schedule<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    [Required]
    public DateTime TriggerOn { get; set; }

    public List<ScheduleDataJson<T>> Data { get; set; } = new();
}
