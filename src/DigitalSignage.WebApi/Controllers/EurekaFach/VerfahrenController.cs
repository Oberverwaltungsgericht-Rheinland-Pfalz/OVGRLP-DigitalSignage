using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
  [RoutePrefix("daten/verfahren")]
  public class VerfahrenController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    public IEnumerable<VerfahrenDto> GetAllVerfahren()
    {
      List<VerfahrenDto> dtos = new List<VerfahrenDto>();

      context.Verfahren
          .Include(v => v.Stammdaten)
          .Include(v => v.ParteienAktiv)
          .Include(v => v.ParteienPassiv)
          .Include(v => v.Besetzung)
          .Include(v => v.ProzBevAktiv)
          .Include(v => v.ProzBevPassiv)
          .Include(v => v.ParteienBeigeladen)
          .Include(v => v.ProzBevBeigeladen)
          .Include(v => v.ParteienZeugen)
          .Include(v => v.ParteienSV)
          .ToList()
          .ForEach(v =>
          {
            dtos.Add(GetDtoFromVerfahren(v));
          });

      return dtos;
    }

    [Route("{id}", Name = "GetVerfahrenById")]
    [HttpGet]
    [ResponseType(typeof(VerfahrenDto))]
    public async Task<IHttpActionResult> GetVerfahren(int id)
    {
      var verfahren = await context.Verfahren.FindAsync(id);
      await context.Entry(verfahren).Reference(v => v.Stammdaten).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ParteienPassiv).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ProzBevAktiv).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
      await context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();

      if (verfahren == null)
      {
        return NotFound();
      }

      return Ok(GetDtoFromVerfahren(verfahren));
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutVerfahren(int id, VerfahrenDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != dto.Id)
      {
        return BadRequest();
      }

      try
      {
        var verfahren = GetVerfahrenFromDto(dto);

        context.Entry(verfahren).State = EntityState.Modified;
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
    [ResponseType(typeof(VerfahrenDto))]
    public async Task<IHttpActionResult> PostVerfahren(VerfahrenDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var verfahren = GetVerfahrenFromDto(dto);

      try
      {
        context.Verfahren.Add(verfahren);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return InternalServerError(ex);
      }

      var newDto = GetDtoFromVerfahren(verfahren);

      return CreatedAtRoute("GetVerfahrenById", new { id = newDto.Id }, newDto);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(Verfahren))]
    public async Task<IHttpActionResult> DeleteVerfahren(int id)
    {
      var verfahren = await context.Verfahren.FindAsync(id);

      if (verfahren == null)
      {
        return NotFound();
      }

      context.Verfahren.Remove(verfahren);
      await context.SaveChangesAsync();

      return Ok(verfahren);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }

    private Verfahren GetVerfahrenFromDto(VerfahrenDto dto)
    {
      return new Verfahren()
      {
        VerfahrensId = dto.Id,
        StammdatenId = dto.StammdatenId,
        Lfdnr = dto.Lfdnr,
        Kammer = dto.Kammer,
        Sitzungssaal = dto.Sitzungssaal,
        SitzungssaalNr = dto.SitzungssaalNr,
        UhrzeitPlan = dto.UhrzeitPlan,
        UhrzeitAktuell = dto.UhrzeitAktuell,
        Status = dto.Status,
        Oeffentlich = dto.Oeffentlich,
        Az = dto.Az,
        Gegenstand = dto.Gegenstand,
        Bemerkung1 = dto.Bemerkung1,
        Bemerkung2 = dto.Bemerkung2,
        Art = dto.Art
      };
    }

    public static VerfahrenDto GetDtoFromVerfahren(Verfahren verfahren)
    {
      string aktivparteiKurz = "";
      if (verfahren.ParteienAktiv.Count > 0)
      {
        aktivparteiKurz = verfahren.ParteienAktiv.First().Partei;
        if (verfahren.ParteienAktiv.Count > 1)
        {
          aktivparteiKurz += " u.a.";
        }
      }

      string passivparteiKurz = "";
      if (verfahren.ParteienPassiv.Count > 0)
      {
        passivparteiKurz = verfahren.ParteienPassiv.First().Partei;
        if (verfahren.ParteienPassiv.Count > 1)
        {
          passivparteiKurz += " u.a.";
        }
      }

      List<string> besetzung = new List<string>();
      foreach (var richter in verfahren.Besetzung)
      {
        besetzung.Add(richter.Richter);
      }

      List<string> aktivParteien = new List<string>();
      foreach (var ap in verfahren.ParteienAktiv)
      {
        aktivParteien.Add(ap.Partei);
      }

      List<string> passivParteien = new List<string>();
      foreach (var pp in verfahren.ParteienPassiv)
      {
        passivParteien.Add(pp.Partei);
      }

      List<string> aktivProzBev = new List<string>();
      foreach (var prozBev in verfahren.ProzBevAktiv)
      {
        aktivProzBev.Add(prozBev.PB);
      }

      List<string> passivProzBev = new List<string>();
      foreach (var prozBev in verfahren.ProzBevPassiv)
      {
        passivProzBev.Add(prozBev.PB);
      }

      List<string> parteienBeigeladen = new List<string>();
      foreach (var beigeladen in verfahren.ParteienBeigeladen)
      {
        parteienBeigeladen.Add(beigeladen.Partei);
      }

      List<string> prozBevBeigeladen = new List<string>();
      foreach (var prozBev in verfahren.ProzBevBeigeladen)
      {
        prozBevBeigeladen.Add(prozBev.PB);
      }

      List<string> parteienZeugen = new List<string>();
      foreach (var partei in verfahren.ParteienZeugen)
      {
        parteienZeugen.Add(partei.Partei);
      }

      List<string> parteienSv = new List<string>();
      foreach (var partei in verfahren.ParteienSV)
      {
        parteienSv.Add(partei.Partei);
      }

      return new VerfahrenDto()
      {
        Id = verfahren.VerfahrensId,
        StammdatenId = verfahren.StammdatenId,
        Lfdnr = verfahren.Lfdnr,
        Az = verfahren.Az,
        Kammer = verfahren.Kammer,
        Sitzungssaal = verfahren.Sitzungssaal,
        SitzungssaalNr = verfahren.SitzungssaalNr,
        UhrzeitPlan = verfahren.UhrzeitPlan,
        UhrzeitAktuell = verfahren.UhrzeitAktuell,
        Status = verfahren.Status,
        Oeffentlich = verfahren.Oeffentlich,
        Gegenstand = verfahren.Gegenstand,
        Bemerkung1 = verfahren.Bemerkung1,
        Bemerkung2 = verfahren.Bemerkung2,
        ParteienAktivKurz = aktivparteiKurz,
        ParteienAktiv = aktivParteien,
        ProzBevAktiv = aktivProzBev,
        ParteienPassivKurz = passivparteiKurz,
        ParteienPassiv = passivParteien,
        ProzBevPassiv = passivProzBev,
        ParteienBeigeladen = parteienBeigeladen,
        ProzBevBeigeladen = prozBevBeigeladen,
        ParteienZeugen = parteienZeugen,
        ParteienSv = parteienSv,
        Art = verfahren.Art,
        Gericht = verfahren.Stammdaten.Gerichtsname,
        Datum = verfahren.Stammdaten.Datum,
        Besetzung = besetzung
      };
    }
  }
}