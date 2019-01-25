using System;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class ParteienBeteiligt
  {
    public int ParteiId { get; set; }
    public Int64 VerfahrensId { get; set; }
    public string Partei { get; set; }
  }
}