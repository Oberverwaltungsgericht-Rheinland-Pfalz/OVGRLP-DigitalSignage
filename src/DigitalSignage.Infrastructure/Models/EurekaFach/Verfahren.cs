using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class Verfahren
  {
    public int VerfahrensId { get; set; }
    public int StammdatenId { get; set; }
    public virtual Stammdaten Stammdaten { get; set; }
    public byte Lfdnr { get; set; }
    public byte Kammer { get; set; }
    public string Sitzungssaal { get; set; }
    public Nullable<Int32> SitzungssaalNr { get; set; }
    public string UhrzeitPlan { get; set; }
    public string UhrzeitAktuell { get; set; }
    public string Status { get; set; }
    public string Oeffentlich { get; set; }
    public string Az { get; set; }
    public string Gegenstand { get; set; }
    public string Bemerkung1 { get; set; }
    public string Bemerkung2 { get; set; }
    public string Art { get; set; }
    public virtual ICollection<Besetzung> Besetzung { get; set; }
    public virtual ICollection<ParteienAktiv> ParteienAktiv { get; set; }
    public virtual ICollection<ParteienPassiv> ParteienPassiv { get; set; }
    public virtual ICollection<ParteienBeigeladen> ParteienBeigeladen { get; set; }
    public virtual ICollection<ParteienSV> ParteienSV { get; set; }
    public virtual ICollection<ParteienZeugen> ParteienZeugen { get; set; }
    public virtual ICollection<ProzBevAktiv> ProzBevAktiv { get; set; }
    public virtual ICollection<ProzBevBeigeladen> ProzBevBeigeladen { get; set; }
    public virtual ICollection<ProzBevPassiv> ProzBevPassiv { get; set; }
  }
}
