// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.WebApi.Controllers.Settings;

namespace DigitalSignage.dn.WebApiCore.DtoModels;

public class BasicPermissions
{
    public bool AllowDisplays { get; set; } = false;
    public bool AllowDisplaysControl { get; set; } = false;
    public Restriction AllowTermine { get; set; } = Restriction.forbidden;
    public Restriction AllowNotes { get; set; } = Restriction.forbidden;
}

public enum Restriction
{ 
    forbidden = 0, 
    read = 1, 
    write = 2 
};