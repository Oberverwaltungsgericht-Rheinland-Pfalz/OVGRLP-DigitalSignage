using System;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class Objekte
  {
    public int ObjektId { get; set; }
    public Int64 VerfahrensId { get; set; }
    public string Objektart { get; set; }
    public string Gemarkung { get; set; }
    public string Flur { get; set; }
    public string Wirtschaftsart { get; set; }
    public string Anschrift { get; set; }
    public string Groesse { get; set; }
    public string Objekt { get; set; }
    public string Blatt { get; set; }
    public string Grundbuchamt { get; set; }
    public string Zusatz { get; set; }
    public string Eigentumsart { get; set; }
    public string Nutzungsrecht { get; set; }
    public string Kurzname { get; set; }
    public decimal Verkehrswert { get; set; }
    public string Grundbuchbezirk { get; set; }
    public string Eigentumsanteil { get; set; }
    public string Schiffsregisterart { get; set; }
    public string Schiffsname { get; set; }
    public string IMONR { get; set; }
    public string RegisterGericht { get; set; }
  }
}