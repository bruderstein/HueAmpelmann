using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.UI.WebControls;

namespace BuildHueService
{
  [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.Single
  )]
    public class ConfigChange : IConfigChange
    {
        public ConfigChange()
        {
            
        }

        public void SetBuildConfig(string build, string htmlColour, string commaSeperatedLights)
        {
            HueConfiguration.Instance.SetBuildConfiguration(
                new BuildConfiguration()
                {
                    Name = build,
                    Colour = new LightColour(Int32.Parse(htmlColour)),
                    Lights = new List<int>(commaSeperatedLights.Split(',').Select(i => Int32.Parse(i)))
                });

        }

        public BuildConfigurationsJson GetBuildConfigs()
        {
            BuildConfigurationsJson configs = new BuildConfigurationsJson();
            configs.BuildConfigurations = new List<BuildConfigurationJson>(
                HueConfiguration.Instance.BuildConfigurations.Select(bc =>
                new BuildConfigurationJson() {
                    HtmlColour = bc.Value.Colour.Hue.ToString(),
                    Lights = new List<int>(bc.Value.Lights),
                    Name = bc.Value.Name }));
            return configs;
        }
    }
}
