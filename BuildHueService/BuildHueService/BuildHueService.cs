using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using TeamCityTrafficLightsConfigurator.Management;

namespace BuildHueService
{
    public partial class BuildHueService : SmartServiceBase
    {
        public BuildHueService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            var stateNotify = new StateNotify(new RuleProcessor());
            WebServiceHost host = new WebServiceHost(stateNotify);
            WebServiceHost configHost = new WebServiceHost(new ConfigChange());

            HueConfiguration.Instance.ResultStates[BuildResult.Success] = new LightColour(25500);
            HueConfiguration.Instance.ResultStates[BuildResult.Fail] = new LightColour(0);
            HueConfiguration.Instance.ResultStates[BuildResult.FailTests] = new LightColour(48600);

            // TODO: Get the actual light ids
            HueConfiguration.Instance.LightIds.AddRange(new [] { 1, 2, 3});
            HueConfiguration.Instance.SetBuildConfiguration(new BuildConfiguration()
            {
                Colour = new LightColour(45000),
                Lights = new List<int>(new[] {2}),
                Name = "bt2"
            });

            // Send the initial "greens"
            foreach (var lightId in HueConfiguration.Instance.LightIds)
            {
                SchedulerManager.Instance.CreateScheduler(lightId, 1000, "http://192.168.1.134", "coolDeveloper");
            }
            stateNotify.RunRuleProcess();
            host.Open();
            configHost.Open();
        }

        protected override void OnStop()
        {
        }
    }
}
