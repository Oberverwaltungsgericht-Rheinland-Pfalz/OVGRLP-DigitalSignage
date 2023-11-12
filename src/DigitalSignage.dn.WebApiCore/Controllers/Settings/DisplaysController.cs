using DigitalSignage.Data;
using DigitalSignage.dn.WebApiCore.DtoModels;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Controllers.EurekaFach;
using DigitalSignage.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;

namespace DigitalSignage.WebApi.Controllers.Settings;

[Route("settings/displays")]
public class DisplaysController : Controller
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
    displaysEx.Add(DisplayDto.FromDisplay(disp, displayManagementService));
  }

  return displaysEx;
}

[Route("{name}", Name = "GetDisplay")]
[HttpGet]
public async Task<ActionResult> GetDisplay(string name)
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

[Route("{name}/DisplayEx", Name = "GetDisplayEx")]
[HttpGet]
public async Task<ActionResult> GetDisplayEx(string name)
{
  var display = await context.Displays
    .FirstAsync(
      d => d.Name == name);

  if (display == null)
    return NotFound();

  DisplayDto displayDto = DisplayDto.FromDisplay(display, displayManagementService);

  return Ok(displayDto);
}

[Route("{name}/termine")]
[HttpGet]
public async Task<ActionResult<IEnumerable<VerfahrenDto>>> GetAllTermine(string name)
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
            context.Entry(v).Collection("ParteienBeteiligt").Load();
            context.Entry(v).Collection("Objekte").Load();
            dtos.Add(new VerfahrenDto(v));
          }
  );

  return Ok(dtos);
}

[Route("{name}/activenotes")]
public async Task<ActionResult<IEnumerable<Note>>> GetActiveNotes(string name, DateTime? timestamp)
{
  DateTime validTimestamp = timestamp.GetValueOrDefault(DateTime.Now);
  var display = await context.Displays
    .Include(d => d.NotesAssignments.Select(na => na.Note))
    .FirstAsync(
      d => d.Name == name);

  if (display == null)
    return NotFound();

  return Ok(display.NotesAssignments.Where(na => IsActiveNoteAssignment(na, validTimestamp)).Select(na => na.Note));
}

private static bool IsActiveNoteAssignment(NoteAssignment noteAssignment, DateTime dateTime)
{
  bool erval = true;

  if (noteAssignment.Start.HasValue && noteAssignment.Start.Value > dateTime)
    erval = false;

  if (noteAssignment.End.HasValue && noteAssignment.End.Value < dateTime)
    erval = false;

  return erval;
}

[Route("{name}/status")]
[HttpGet]
public async Task<ActionResult<DisplayStatus>> GetStatusForDisplay(string name)
{
  var display = await context.Displays.FirstAsync(
    d => d.Name == name);

  if (display == null)
    return NotFound();

  return Ok(displayManagementService.GetDisplayStatus(display));
}

[Route("{name}/start")]
[HttpGet]
public async Task<ActionResult> StartDisplay(string name)
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
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

  return Ok();
}

[Route("{name}/restart")]
[HttpGet]
public async Task<ActionResult> RestartDisplay(string name)
{
  var display = await context.Displays.FirstAsync(
    d => d.Name == name);

  if (display == null)
    return NotFound();

  try
  {
    await displayManagementService.RestartDisplay(display);
  }
  catch (Exception ex)
  {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok();
}

[Route("{name}/stop")]
[HttpGet]
public async Task<ActionResult> StopDisplay(string name)
{
  var display = await context.Displays.FirstAsync(
    d => d.Name == name);

  if (display == null)
    return NotFound();

  try
  {
    await displayManagementService.StopDisplay(display);
  }
  catch (Exception ex)
  {
      return StatusCode(StatusCodes.Status500InternalServerError, ex);
  }

   return Ok();
}

[Route("{name}/ScreenshotUrl")]
[HttpGet]
public async Task<ActionResult> GetScreenshotUrl(string name)
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
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(url);
}

[Route("{name}")]
[HttpPut]
public async Task<ActionResult> PutDisplay(string name, Display display)
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return NoContent();
}

[Route("")]
[HttpPost]
public async Task<ActionResult> PostDisplay(Display display)
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return CreatedAtRoute("GetDisplay", new { name = display.Name }, display);
}

[Route("{name}")]
[HttpDelete]
public async Task<ActionResult<Display>> DeleteDisplay(string name)
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

}
