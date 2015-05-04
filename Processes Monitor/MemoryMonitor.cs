using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processes_Monitor
{
    public partial class MemoryMonitor : Form
    {
        private ProcessesStateInfo.ProcessInfo processInfo;
        public MemoryMonitor(ProcessesStateInfo.ProcessInfo processInfo)
        {
            this.processInfo = processInfo;
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void MemoryMonitor_Load(object sender, EventArgs e)
        {
            this.Name = String.Format("{0}({1})",processInfo.Name,processInfo.Id);
            var memory = processInfo.MemoryOccupancy;
            var doubleMemory = memory * 2;
            this.monitorControl1.MonitorView(Convert.ToDecimal(memory), Convert.ToDecimal(doubleMemory));
            Thread monitorThread = new Thread(() =>
            {
                while(true)
                {
                    Thread.Sleep(1000);
                    this.RefreshMonitorView(ref doubleMemory);
                }
            });
            monitorThread.IsBackground = true;
            monitorThread.Start();
        }

        private void RefreshMonitorView(ref float doubleMemory)
        {
            var memory = processInfo.RefreshProcessMemory();
            if (memory >= doubleMemory)
            {
                doubleMemory = doubleMemory * 2;
            }
            this.monitorControl1.MonitorView(Convert.ToDecimal(memory), Convert.ToDecimal(doubleMemory));
        }

    }
}
