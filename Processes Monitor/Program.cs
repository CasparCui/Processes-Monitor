using System;
using System.Windows.Forms;

namespace Processes_Monitor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var process = ProcessesMonitorConfig.DestinationFolderPath;
            //process.GetProcessOccupancyFilePath(948);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProcessesMonitor());
        }
    }
}