using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace BuildHueService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Contains("/desktop"))
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DesktopServiceForm(new BuildHueService()));
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new BuildHueService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
