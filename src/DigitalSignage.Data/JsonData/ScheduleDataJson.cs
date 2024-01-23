// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.Data.JsonData;

public class ScheduleDataJson<T>
    where T : struct
{
    public T Id { get; set; }
    public string Action { get; set; } = "";
    public List<T> Targets { get; set; } = new();
}
