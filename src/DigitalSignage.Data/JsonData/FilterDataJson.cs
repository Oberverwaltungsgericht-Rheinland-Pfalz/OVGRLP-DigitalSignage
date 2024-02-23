// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
global using System;

namespace DigitalSignage.Data.JsonData;
public enum FilterMode
{
    Exclusive = 0, // Only Display the given Filterd Entities
    Subtractive = 1, // Only Hide the given Filterd Entities
}

public enum FilterType // The given Target-Ids are ...
{
    Rooms = 0,
    Persons = 1,
    Departments = 2,
    Groups = 3,
    Displays = 4,
    Date = 5,
}

public class FilterDataJson<T>
    where T : struct
{
    public T Id { get; set; }
    // What type of Filter is this for (like filter Rooms, Persons, Departments...)
    public FilterType Type { get; set; } = FilterType.Rooms ;
    // List of Targets to Filter (all ids of the Targets to include or exclude, depending on FilterMode)
    public List<T> Targets { get; set; } = new();

    public FilterMode FilterMode { get; set; } = FilterMode.Exclusive;

    // Only relevant for Datefilters...
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
