using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalSignage.Infrastructure.Models.Settings
{
    public class Display
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public string Styles { get; set; }
        public string Filter { get; set; }
        public string Group { get; set; }
        public string ControlUrl { get; set; }
        public string NetAddress { get; set; }
        public string WolIpAddress { get; set; }
        public string WolMacAddress { get; set; }
        public int WolUdpPort { get; set; }
        public string Description { get; set; }
    }
}
