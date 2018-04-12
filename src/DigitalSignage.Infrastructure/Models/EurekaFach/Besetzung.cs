using System;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class Besetzung
  {
    public int BesetzungsId { get; set; }
    public Int64 VerfahrensId { get; set; }
    public string Richter { get; set; }
  }
}