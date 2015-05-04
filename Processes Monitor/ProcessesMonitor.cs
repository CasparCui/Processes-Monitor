using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processes_Monitor
{
    public partial class ProcessesMonitor : Form
    {
        private Dictionary<int, ProcessesStateInfo.ProcessInfo> processes;
        private Dictionary<String, ServiceStateInfo.ServiceInfo> services;
        public ProcessesMonitor()
        {

            InitializeComponent();
            this.processes = ProcessesStateInfo.Processes;
            this.services = ServiceStateInfo.WindowsServices;
        }

        private void ProcessesMonitor_Load(object sender, EventArgs e)
        {

            this.processes = ProcessesStateInfo.Processes;
            List<ListViewItem> items = new List<ListViewItem>();
            var doubleRow = false;
            foreach( var process in processes.Values)
            {
                var listViewItem = new ListViewItem(new string[] 
                { 
                    process.Name, 
                    process.Id.ToString(), 
                    process.User, 
                    process.CpuOccupancyRate.ToString(), 
                    process.MomoryOccupancy.ToString("#0.00")+"MB", 
                    process.LocalPath 
                });
                if(doubleRow)
                {
                    listViewItem.BackColor = Color.FromArgb(200, 255, 255);
                    doubleRow = false;
                }
                else
                {
                    doubleRow = true;
                }
                items.Add(listViewItem);
            }
            process_ListView.Items.AddRange(items.ToArray());
            process_ListView.Refresh();
        }
    }
}
