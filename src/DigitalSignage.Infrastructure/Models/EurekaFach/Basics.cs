using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalSignage.Infrastructure.Models.EurekaFach
{
    public class Basics
    {
        public int Nummer { get; set; }
        public string Gerichtsname { get; set; }
        public string Kuerzel { get; set; }
        public string toXMLFullPath { get; set; }
        public string xsltFullPath { get; set; }
        public string globalXMLFullPath { get; set; }
    }
}
