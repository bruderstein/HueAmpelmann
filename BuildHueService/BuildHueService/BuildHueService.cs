using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;

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

            WebServiceHost host = new WebServiceHost(new StateNotify(new RuleProcessor()));
            
            host.Open();
        }

        protected override void OnStop()
        {
        }
    }
}
