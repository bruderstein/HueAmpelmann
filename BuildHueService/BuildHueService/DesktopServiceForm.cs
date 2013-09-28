using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace BuildHueService
{
    public partial class DesktopServiceForm : Form
    {
        private SmartServiceBase m_service;

        public DesktopServiceForm(SmartServiceBase service)
        {
            InitializeComponent();
            m_service = service;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            m_service.StartService(null);
        }
    }
}
