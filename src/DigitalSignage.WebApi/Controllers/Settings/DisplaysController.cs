using AutoMapper;
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Controllers.EurekaFach;
using DigitalSignage.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DigitalSignage.WebApi.Controllers.Settings
{
  [RoutePrefix("settings/displays")]
  public class DisplaysController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();
    private readonly DisplayManagementService displayManagementService = new DisplayManagementService();

    [Route("")]
    [HttpGet]
    public IEnumerable<Display> GetAllDisplays()
    {
      return context.Displays.Where(d => d.Dummy == false);
    }

    [Route("DisplaysEx")]
    [HttpGet]
    public IEnumerable<DisplayDto> GetAllDisplaysEx()
    {
      List<Display> displays = context.Displays.Where(d => d.Dummy == false).ToList();
      List<DisplayDto> displaysEx = new List<DisplayDto>();
      foreach (Display disp in displays)
      {
        DisplayDto dispEx = Mapper.Map<DisplayDto>(disp);
        dispEx.Status = displayManagementService.GetDisplayStatus(disp);
        dispEx.ScreenshotUrl = displayManagementService.GetDisplayScreenshotUrl(disp);
        displaysEx.Add(dispEx);
      }

      return displaysEx;
    }

    [Route("{name}", Name = "GetDisplay")]
    [HttpGet]
    public async Task<IHttpActionResult> GetDisplay(string name)
    {
      var display = await context.Displays
        .FirstAsync(
          d => d.Name == name);

      if (display == null)
        return NotFound();

      var displayDto = new
      {
        Id = display.Id,
        Description = display.Description,
        Name = display.Name,
        Title = display.Title,
        Template = display.Template,
        Styles = display.Styles,
        ControlUrl = display.ControlUrl,
        Group = display.Group
      };

      return Ok(displayDto);
    }

    [Route("{name}/termine")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<VerfahrenDto>))]
    public async Task<IHttpActionResult> GetAllTermine(string name)
    {
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      List<VerfahrenDto> dtos = new List<VerfahrenDto>();

      context.Verfahren
          .SqlQuery("SELECT * FROM Verfahren" + (!String.IsNullOrEmpty(display.Filter) ? " WHERE " + display.Filter : ""))
          .ToList()
          .ForEach(v =>
              {
                context.Entry(v).Reference("Stammdaten").Load();
                context.Entry(v).Collection("ParteienAktiv").Load();
                context.Entry(v).Collection("ParteienPassiv").Load();
                context.Entry(v).Collection("Besetzung").Load();
                context.Entry(v).Collection("ProzBevAktiv").Load();
                context.Entry(v).Collection("ProzBevPassiv").Load();
                context.Entry(v).Collection("ParteienBeigeladen").Load();
                context.Entry(v).Collection("ProzBevBeigeladen").Load();
                context.Entry(v).Collection("ParteienZeugen").Load();
                context.Entry(v).Collection("ParteienSV").Load();
                dtos.Add(VerfahrenController.GetDtoFromVerfahren(v));
              }
      );

      return Ok(dtos);
    }

    [Route("{name}/activenotes")]
    [ResponseType(typeof(IEnumerable<Note>))]
    public async Task<IHttpActionResult> GetActiveNotes(string name)
    {
      var display = await context.Displays
        .Include(d => d.NotesAssignments.Select(na => na.Note))
        .FirstAsync(
          d => d.Name == name);

      if (display == null)
        return NotFound();

      return Ok(display.NotesAssignments.Where(na => IsActiveNoteAssignment(na)).Select(na => na.Note));
    }

    private static bool IsActiveNoteAssignment(NoteAssignment noteAssignment)
    {
      bool erval = true;

      if (noteAssignment.Start.HasValue && noteAssignment.Start.Value > DateTime.Now)
        erval = false;

      if (noteAssignment.End.HasValue && noteAssignment.End.Value < DateTime.Now)
        erval = false;

      return erval;
    }

    [Route("{name}/status")]
    [HttpGet]
    [ResponseType(typeof(DisplayStatus))]
    public async Task<IHttpActionResult> GetStatusForDisplay(string name)
    {
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      return Ok(displayManagementService.GetDisplayStatus(display));
    }

    [Route("{name}/start")]
    [HttpGet]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> StartDisplay(string name)
    {
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      try
      {
        displayManagementService.StartDisplay(display);
      }
      catch
      {
        return InternalServerError();
      }

      return Ok();
    }

    [Route("{name}/restart")]
    [HttpGet]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> RestartDisplay(string name)
    {
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      try
      {
        displayManagementService.RestartDisplay(display);
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok();
    }

    [Route("{name}/stop")]
    [HttpGet]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> StopDisplay(string name)
    {
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      try
      {
        displayManagementService.StopDisplay(display);
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok();
    }

    [Route("{name}/ScreenshotUrl")]
    [HttpGet]
    [ResponseType(typeof(string))]
    public async Task<IHttpActionResult> GetScreenshotUrl(string name)
    {
      string url = string.Empty;
      var display = await context.Displays.FirstAsync(
        d => d.Name == name);

      if (display == null)
        return NotFound();

      try
      {
        url = displayManagementService.GetDisplayScreenshotUrl(display);
      }
      catch
      {
        return InternalServerError();
      }

      return Ok(url);
    }

    [Route("{name}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutDisplay(string name, Display display)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (name != display.Name)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(display).State = EntityState.Modified;
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return InternalServerError(ex);
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

    [Route("")]
    [HttpPost]
    [ResponseType(typeof(Display))]
    public async Task<IHttpActionResult> PostDisplay(Display display)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        context.Displays.Add(display);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetDisplay", new { name = display.Name }, display);
    }

    [Route("{name}")]
    [HttpDelete]
    [ResponseType(typeof(Display))]
    public async Task<IHttpActionResult> DeleteDisplay(string name)
    {
      var display = await context.Displays.FindAsync(name);

      if (display == null)
      {
        return NotFound();
      }

      context.Displays.Remove(display);
      await context.SaveChangesAsync();

      return Ok(display);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }

  public class DisplayDto : Display
  {
    public DisplayStatus Status;
    public string ScreenshotUrl;
  }
}