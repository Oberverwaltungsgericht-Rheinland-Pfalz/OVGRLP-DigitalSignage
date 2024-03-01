// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using AutoMapper;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Services;
using System.ComponentModel.DataAnnotations;

namespace DigitalSignage.dn.WebApiCore.DtoModels;

public class DisplayExDto : DisplayDto
{
    [Required]
    public DisplayStatus Status { get; set; }

    [Required]
    public required string ScreenshotUrl { get; set; } = "";

    public static new DisplayExDto FromDisplay(Display display)
    {
        var displayManagementService = new DisplayManagementService();
        return FromDisplay(display, displayManagementService);
    }

    public static new DisplayExDto FromDisplay(Display display, DisplayManagementService displayManagementService)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Display, DisplayDto>());
        var mapper = new Mapper(config);
        DisplayExDto dispEx = mapper.Map<DisplayExDto>(display);

        dispEx.Status = displayManagementService.GetDisplayStatus(display);
        dispEx.ScreenshotUrl = displayManagementService.GetDisplayScreenshotUrl(display);
        return dispEx;
    }
}

public class DisplayDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required] 
    public required string Title { get; set; }

    [Required(AllowEmptyStrings = true)]
    public required string Template { get; set; }

    public string? Styles { get; set; }

    public string? Filter { get; set; }

    [Required]
    public required string Group { get; set; }

    public string? ControlUrl { get; set; }

    [Required]
    public required string NetAddress { get; set; }

    [Required]
    public required string WolIpAddress { get; set; }

    [Required] 
    public required string WolMacAddress { get; set; }

    [Required]
    public int WolUdpPort { get; set; } = 0;

    [Required(AllowEmptyStrings = true)]
    public required string Description { get; set; }
    
    [Required] 
    public ICollection<NoteAssignment>? NotesAssignments { get; set; }
    
    [Required] 
    public bool Dummy { get; set; }

    public static DisplayDto FromDisplay(Display display)
    {
        var displayManagementService = new DisplayManagementService();
        return FromDisplay(display, displayManagementService);
    }

    public static DisplayDto FromDisplay(Display display, DisplayManagementService displayManagementService)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Display, DisplayDto>());
        var mapper = new Mapper(config);
        DisplayDto dispEx = mapper.Map<DisplayExDto>(display);

        return dispEx;
    }
}