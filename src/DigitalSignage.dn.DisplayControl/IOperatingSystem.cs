// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.dn.DisplayControl;

public interface IOperatingSystem
{
    IResult Restart();

    IResult Shutdown();

    Task Screenshot(HttpContext context);
}