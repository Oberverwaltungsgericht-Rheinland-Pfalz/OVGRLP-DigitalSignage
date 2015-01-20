using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
    public class Besetzung
    {
        public int BesetzungsId { get; set; }
        public int VerfahrensId { get; set; }
        public string Richter { get; set; }
    }
}
