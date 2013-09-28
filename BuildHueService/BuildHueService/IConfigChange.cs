using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BuildHueService
{
    [ServiceContract]
    public interface IConfigChange
    {
        // /buildConfig/the_build_name/ff0000/1,2   
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "{build}/{htmlColour}/{commaSeperatedLights}")]
        void SetBuildConfig(string build, string htmlColour, string commaSeperatedLights);

        // /buildConfig/getAll
        [OperationContract]
        [WebGet(UriTemplate = "getAll")]
        BuildConfigurationsJson GetBuildConfigs();
    }

    public class BuildConfigurationsJson
    {
        public List<BuildConfigurationJson> BuildConfigurations { get; set; }
    }

    public class BuildConfigurationJson
    {
        public string Name { get; set; }

        public string HtmlColour { get; set; }

        public List<int> Lights { get; set; }
    }
}