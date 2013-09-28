using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BuildHueService
{
    public class BuildConfiguration
    {
        public string Name { get; set; }
        public LightColour Colour { get; set; }
        public IList<int> Lights { get; set; }
    }
}
