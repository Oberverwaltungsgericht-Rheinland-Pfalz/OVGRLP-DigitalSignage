// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

namespace DigitalSignage.Data.DbV3Models;

/// <summary>
/// Base Model for all Entities. This is necessary for Entity Framework
/// to determine common Properties of all the Models (mostly the Id).
/// </summary>
public interface IBaseModel<T> where T : struct
{
    public T Id { get; set; }
}
