using System;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class ParteienBeigeladen
  {
    public int ParteiId { get; set; }
    public Int64 VerfahrensId { get; set; }
    public string Partei { get; set; }
  }
}