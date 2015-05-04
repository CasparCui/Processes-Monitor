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
                    process.MemoryOccupancy.ToString("#0.00")+"MB", 
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
            this.process_ContextMenu.Enabled = false;
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
                        item.SubItems[4].Text = processes[Convert.ToInt32(item.SubItems[1].Text)].MemoryOccupancy.ToString("#0.00") + "MB";
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
                            process.MemoryOccupancy.ToString("#0.00")+"MB", 
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

        private void process_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.process_ContextMenu.Enabled = true;
            var listViewItems = process_ListView.SelectedItems;
            if(listViewItems.Count==1)
            {
                this.goToSerivceToolStripMenuItem.Enabled = true;
            }
            else if(listViewItems.Count==0)
            {
                this.process_ContextMenu.Enabled = false;
            }
            else
            {
                this.goToSerivceToolStripMenuItem.Enabled = false;
            }
        }

        private void endTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItems = process_ListView.SelectedItems;
            {
                foreach(ListViewItem item in listViewItems)
                {
                    var processControl = new ProcessControl();
                    if(processControl.KillProcess(Convert.ToInt32(item.Name)))
                    {
                        item.Remove();
                    }
                    else
                    {
                        MessageBox.Show("Error", processControl.ProcessException.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process_ListView.Refresh();
        }

        private void goToSerivceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItems = process_ListView.SelectedItems;
            var pId_String = listViewItems[0].Name;
            this.tabControl1.SelectedTab = tabService;
            foreach(ListViewItem item in service_ListView.Items)
            {
                if(!String.IsNullOrEmpty( item.SubItems[3].Text) && item.SubItems[3].Text.Equals(pId_String,StringComparison.OrdinalIgnoreCase))
                {
                    item.Selected = true;
                }
                else
                {
                    service_ListView.SelectedItems.Clear();
                }
            }
        }

        private void service_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listviewItems = service_ListView.SelectedItems;
            if(listviewItems.Count!=0)
            {
                this.service_ContextMenu.Enabled = true;
                var state_String = listviewItems[0].SubItems[2].Text;
                switch(state_String)
                {
                    case "Running":
                        this.startToolStripMenuItem.Enabled = false;
                        this.reStartToolStripMenuItem.Enabled = true;
                        this.stopToolStripMenuItem.Enabled = true;
                        break;
                    case "Stopped":
                        this.startToolStripMenuItem.Enabled = true;
                        this.reStartToolStripMenuItem.Enabled = false;
                        this.stopToolStripMenuItem.Enabled = false;
                        break;
                    default:
                        this.startToolStripMenuItem.Enabled = false;
                        this.reStartToolStripMenuItem.Enabled = false;
                        this.stopToolStripMenuItem.Enabled = false;
                        break;
                }
                if(!String.IsNullOrEmpty(listviewItems[0].SubItems[3].Text))
                {
                    this.goToSerivceToolStripMenuItem.Enabled = true;
                }
                else
                {
                    this.goToSerivceToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItem = service_ListView.SelectedItems[0];
            if (ServiceControl.ControlService(listViewItem.Name, ServiceControlOption.Start))
            {
                listViewItem.SubItems[2].Text = ServiceRunningState.Start_Pending.ToString();
            }
            else
            {
                MessageBox.Show("Error", ServiceControl.ServiceException.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItem = service_ListView.SelectedItems[0];
            if (ServiceControl.ControlService(listViewItem.Name, ServiceControlOption.Restart))
            {
                listViewItem.SubItems[2].Text = ServiceRunningState.Stop_Pending.ToString();
            }
            else
            {
                MessageBox.Show("Error", ServiceControl.ServiceException.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItem = service_ListView.SelectedItems[0];
            if (ServiceControl.ControlService(listViewItem.Name, ServiceControlOption.Stop))
            {
                listViewItem.SubItems[2].Text = ServiceRunningState.Stop_Pending.ToString();
            }
            else
            {
                MessageBox.Show("Error", ServiceControl.ServiceException.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void goToProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItem = service_ListView.SelectedItems[0];
            var pID = listViewItem.SubItems[3].Text;
            tabControl1.SelectedTab = tabProcess;
            if (process_ListView.Items.ContainsKey(pID))
            {
                process_ListView.Items.Find(pID, false)[0].Selected = true;
            }
        }

        private void showMomoryMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listViewItem = this.process_ListView.SelectedItems[0];
            var processInfo = new ProcessesStateInfo.ProcessInfo(Process.GetProcessById(Convert.ToInt32(listViewItem.Name)));
            var monitorFormWindow = new MemoryMonitor(processInfo);
            monitorFormWindow.Show();
        }

        private void moveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

    }
}
