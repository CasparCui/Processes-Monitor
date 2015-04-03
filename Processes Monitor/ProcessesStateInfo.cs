using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace Processes_Monitor
{
    class ProcessesStateInfo
    {
        static private Dictionary<int, ProcessInfo> processes;
        static public Dictionary<int,ProcessInfo> GetProcessesStateInfo()
        {
            processes = new Dictionary<int, ProcessInfo>();
            foreach(Process process in Process.GetProcesses())
            {
                processes.Add(process.Id ,new ProcessInfo(process));
            }
            return processes;
        }
        static private Dictionary<int, ProcessInfo> RefreshProcessStateInfo()
        {
            if(processes==null||processes.Count==0)
            {
                return GetProcessesStateInfo();
            }
            var tempProcesses = new Dictionary<int, ProcessInfo>();
            foreach(Process process in Process.GetProcesses())
            {
                if(!processes.ContainsKey(process.Id))
                {
                    processes.Add(process.Id, new ProcessInfo(process));
                }
                processes[process.Id].Refreshed = true;
            }
            for(int i = 0;i<processes.Count;i++)
            {
                if(processes[i].Refreshed)
                {
                    processes[i].Refreshed = false;
                }
                else
                {
                    processes.Remove(processes[i].Id);
                    i--;
                }
            }
            return processes;
            
        }
        public class ProcessInfo
        {
            private int mId;
            private String mName;
            private double mCpuOccupancyRate;
            private String mLocalPath;
            private String mUser;
            private double mMemoryOccupancy;
            private String mDiscription;
            private ManagementObjectSearcher searcher;
            private ManagementObjectCollection processDataCache;

            public bool Refreshed { get; set; }
            public int Id { get { return this.mId; } }
            public String Name { get { return this.mName; } }
            public double CpuOccupancyRate
            {
                get { return this.mCpuOccupancyRate; }
                set { this.mCpuOccupancyRate = value; }
            }
            public String LocalPath { get { return this.mLocalPath; } }
            public String User { get { return this.mUser; } }
            public double MomoryOccupancy
            {
                get { return this.mMemoryOccupancy; }
                set { this.mMemoryOccupancy = value; }
            }
            public string Discription { get { return this.mDiscription; } }

            public ProcessInfo(Process process)
            {
                Refreshed = false;
                this.mId = process.Id;
                this.mName = process.ProcessName;
                searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ProcessID = '" + this.mId + "'");
                processDataCache = searcher.Get();
                foreach (ManagementObject processDataObj in processDataCache)
                {
                    this.mLocalPath = (String)processDataObj["ExecutablePath"];
                    this.mDiscription = (String)processDataObj["Caption"];
                    this.mUser = GetProcessOwner(processDataObj);
                }
                this.mCpuOccupancyRate = 0;
                this.mMemoryOccupancy = 0;
            }
            private String GetProcessOwner(ManagementObject processDataObj)
            {
                try
                {
                    object[] ownerInfo = new object[2];
                    uint resultNumber = (uint)processDataObj.InvokeMethod("GetOwner", ownerInfo);
                    if (resultNumber != 0)
                    {
                        return String.Format("{0}\\{1}", (String)ownerInfo[0], (String)ownerInfo[1]);
                    }
                    else
                    {
                        return "SYSTEM";
                    }
                }
                catch
                {
                    return "SYSTEM";
                }
            }

        }


    }
}
