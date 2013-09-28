using System.Collections.Generic;

namespace BuildHueService
{
    public class LightStatus
    {
        public LightStatus(int lightId)
        {
            LightId = lightId;
            Colours = new List<LightColour>();
        }

        public int LightId { get; private set; }
        public List<LightColour> Colours { get; private set; } 
    }
}