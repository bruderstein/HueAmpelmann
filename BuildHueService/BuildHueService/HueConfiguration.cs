using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BuildHueService
{
    public class HueConfiguration
    {
        private static HueConfiguration s_instance;

        public static HueConfiguration Instance
        {
            get
            {
                if (null == s_instance)
                {
                    s_instance = new HueConfiguration();
                }
                return s_instance;
            }
        }


        public static void ClearConfiguration()
        {
            s_instance = null;
        }

        private HueConfiguration()
        {
            ResultStates = new Dictionary<BuildResult, LightColour>();
            BuildConfigurations = new Dictionary<string, BuildConfiguration>(StringComparer.InvariantCultureIgnoreCase);
            LightIds = new List<int>();
        }

        public void SetBuildConfiguration(BuildConfiguration buildConfiguration)
        {
            BuildConfigurations[buildConfiguration.Name] = buildConfiguration;
        }


        public void SetLightIds(IEnumerable<int> lightIds)
        {
            LightIds = new List<int>(lightIds);
        }

        public Dictionary<BuildResult, LightColour> ResultStates { get; private set; } 
        public Dictionary<string, BuildConfiguration> BuildConfigurations { get; private set; }
        public List<int> LightIds { get; private set; } 
    }
}
