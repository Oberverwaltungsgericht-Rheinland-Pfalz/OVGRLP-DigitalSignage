﻿using System.Collections.Generic;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
  public class Stammdaten
  {
    public int StammdatenId { get; set; }
    public string Gerichtsname { get; set; }
    public string Datum { get; set; }
    public virtual ICollection<Verfahren> Verfahren { get; set; }
  }
}