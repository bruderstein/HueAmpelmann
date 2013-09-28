using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BuildHueService
{
    public class LightColour
    {
        private double m_x;
        private double m_y;
        private int m_hue;

        public LightColour()
        {
            // White, ish!
            m_x = 0.35;
            m_y = 0.35;
        }

        public LightColour(int hue)
        {
            m_hue = hue;

        }


        public LightColour(Color color)
        {

            double red = color.R/255.0;
            double green = color.G/255.0;
            double blue = color.B/255.0;
            red = (red > 0.04045f) ? Math.Pow((red + 0.055f) / (1.0f + 0.055f), 2.4f) : (red / 12.92f);
            green = (green > 0.04045f) ? Math.Pow((green + 0.055f) / (1.0f + 0.055f), 2.4f) : (green / 12.92f);
            blue = (blue > 0.04045f) ? Math.Pow((blue + 0.055f) / (1.0f + 0.055f), 2.4f) : (blue / 12.92f);

            double X = red * 0.649926f + green * 0.103455f + blue * 0.197109f;
            double Y = red * 0.234327f + green * 0.743075f + blue * 0.022598f;
            double Z = red * 0.0000000f + green * 0.053077f + blue * 1.035763f;

            m_x = X / (X + Y + Z); 
            m_y = Y / (X + Y + Z);
        }

        public double X
        {
            get { return m_x; }
        }

        public double Y
        {
            get { return m_y; }
        }

        public int Hue
        {
            get { return m_hue; }
        }
    }
}
