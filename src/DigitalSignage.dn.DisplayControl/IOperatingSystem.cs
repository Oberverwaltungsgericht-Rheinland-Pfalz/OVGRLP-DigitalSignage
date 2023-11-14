// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.AspNetCore.Http.HttpResults;

namespace DigitalSignage.dn.DisplayControl;
interface IOperatingSystem
{
    Task<Ok<string>> Restart();
    Task<Ok<string>> Shutdown();
    Task Screenshot(HttpContext context);
}
