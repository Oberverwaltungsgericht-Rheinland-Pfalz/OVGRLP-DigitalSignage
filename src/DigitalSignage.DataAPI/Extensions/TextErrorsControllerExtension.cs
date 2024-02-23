// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.DataAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Extensions;

public static class TextHelperControllerExtension
{
    public static NotFoundObjectResult NotFoundReturn(this Controller controller, string Type)
    {
        return controller.NotFound($"Did not find ${Type} with the given Id");
    }

    public static BadRequestObjectResult AlreadyExists(this Controller controller, string Type)
    {
        return controller.BadRequest($"${Type} with this Id already exists");
    }

    public static BadRequestObjectResult WrongFormat(this Controller controller, string Type)
    {
        return controller.BadRequest($"${Type} is not in an supported format or empty");
    }

    public static BadRequestObjectResult EmptyField(this Controller controller, string Type, string Field)
    {
        return controller.BadRequest($"${Type} must have a value for ${Field}");
    }

    public static BadRequestObjectResult WrongContent(this Controller controller, string Parameter)
    {
        return controller.BadRequest($"Parameter ${Parameter} has wrong content or led to an error");
    }
}
