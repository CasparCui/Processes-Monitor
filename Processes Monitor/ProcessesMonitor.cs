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
        object lockobj = new object();
        private Dictionary<int, ProcessesStateInfo.ProcessInfo> processes;
        private Dictionary<String, ServiceStateInfo.ServiceInfo> services;
        public ProcessesMonitor()
        {

            InitializeComponent();
            this.processes = ProcessesStateInfo.Processes;
            this.services = ServiceStateInfo.WindowsServices;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void ProcessesMonitor_Load(object sender, EventArgs e)
        {

            this.processes = ProcessesStateInfo.Processes;
            List<ListViewItem> processItems = new List<ListViewItem>();
            var doubleRow = false;
            foreach (var process in processes.Values)
            {
                var listViewItem = new ListViewItem(new string[] 
                { 
                    process.Name, 
                    process.Id.ToString(), 
                    process.User, 
                    process.CpuOccupancyRate.ToString(),//暂时不work，原因不明。 
                    process.MomoryOccupancy.ToString("#0.00")+"MB", 
                    process.LocalPath 
                });
                listViewItem.Name = process.Id.ToString();
                if (doubleRow)
                {
                    listViewItem.BackColor = Color.FromArgb(200, 255, 255);
                    doubleRow = false;
                }
                else
                {
                    doubleRow = true;
                }
                processItems.Add(listViewItem);
            }
            process_ListView.Items.AddRange(processItems.ToArray());
            process_ListView.Refresh();
            List<ListViewItem> serviceItems = new List<ListViewItem>();
            doubleRow = false;
            foreach (var service in services.Values)
            {
                var listViewItem = new ListViewItem(new string[]
                    {
                        service.Name,
                        service.Description,
                        service.ServiceState.ToString(),
                        service.ProcessID==0 ? string.Empty : service.ProcessID.ToString(),
                    });
                listViewItem.Name = service.Name;
                if (doubleRow)
                {
                    listViewItem.BackColor = Color.FromArgb(200, 255, 255);
                    doubleRow = false;
                }
                else
                {
                    doubleRow = true;
                }
                serviceItems.Add(listViewItem);
            }
            service_ListView.Items.AddRange(serviceItems.ToArray());
            service_ListView.Refresh();
            Thread refreshListViewThread = new Thread(() =>
            {
                do
                {
                    Thread.Sleep(1500);
                }
                while (this.refreshListViews(process_ListView.Items, service_ListView.Items));

            });
            refreshListViewThread.IsBackground = true;
            refreshListViewThread.Start();
        }

        private bool refreshListViews(ListView.ListViewItemCollection processView, ListView.ListViewItemCollection serviceView)
        {
            this.processes = ProcessesStateInfo.Processes;
            this.services = ServiceStateInfo.WindowsServices;
            lock (lockobj)
            {
                foreach (ListViewItem item in processView)
                {
                    if (processes.ContainsKey(Convert.ToInt32(item.SubItems[1].Text/*PID*/)))
                    {
                        item.SubItems[3].Text = processes[Convert.ToInt32(item.SubItems[1].Text)].CpuOccupancyRate.ToString();
                        item.SubItems[4].Text = processes[Convert.ToInt32(item.SubItems[1].Text)].MomoryOccupancy.ToString("#0.00") + "MB";
                    }
                    else
                    {
                        item.Remove();
                    }
                }
                foreach (var process in processes.Values)
                {
                    if (!processView.ContainsKey(process.Id.ToString()))
                    {
                        processView.Add(new ListViewItem(new string[] 
                        {
                            process.Name, 
                            process.Id.ToString(), 
                            process.User, 
                            process.CpuOccupancyRate.ToString(),//暂时不work，原因不明。 
                            process.MomoryOccupancy.ToString("#0.00")+"MB", 
                            process.LocalPath 
                        })
                        {
                            Name = process.Id.ToString(),
                        });
                    }
                }
                foreach (ListViewItem item in serviceView)
                {
                    if (services.ContainsKey(item.SubItems[0].Text))
                    {
                        item.SubItems[2].Text = services[item.SubItems[0].Text].ServiceState.ToString();
                    }
                }
                return true;
            }
        }
    }
}
