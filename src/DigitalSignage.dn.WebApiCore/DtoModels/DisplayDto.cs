// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using AutoMapper;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Services;

namespace DigitalSignage.dn.WebApiCore.DtoModels;

public class DisplayDto : Display
{
    public DisplayStatus Status;
    public string ScreenshotUrl;

     public static DisplayDto FromDisplay(Display display)
    {
        var displayManagementService = new DisplayManagementService();
        return FromDisplay(display, displayManagementService);
    }

    public static DisplayDto FromDisplay(Display display, DisplayManagementService displayManagementService)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Display, DisplayDto>());
        var mapper = new Mapper(config);
        DisplayDto dispEx = mapper.Map<DisplayDto>(display);

        dispEx.Status = displayManagementService.GetDisplayStatus(display);
        dispEx.ScreenshotUrl = displayManagementService.GetDisplayScreenshotUrl(display);
        return dispEx;
    }
}