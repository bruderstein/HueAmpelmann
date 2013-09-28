using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace BuildHueService
{
    public class SmartServiceBase : ServiceBase
    {
        public void StartService(string[] args)
        {
            this.OnStart(args);
        }

        public void StopService()
        {
            this.OnStop();
        }
    }
}

