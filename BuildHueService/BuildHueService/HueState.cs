namespace BuildHueService
{
    public class HueState
    {
        public HueState(string colour, HueEffect hueEffect)
        {
            ColourString = colour;
            HueEffect = hueEffect;
        }

        public string ColourString { get; set; }
        public HueEffect HueEffect { get; set; }
    }
}