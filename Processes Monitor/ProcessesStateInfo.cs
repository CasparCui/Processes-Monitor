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
        /// <summary>
        /// 
        /// </summary>
        static public Dictionary<int, ProcessInfo> Processes
        {
            get
            {
                return RefreshProcessStateInfo();
            }
        }
        /// <summary>
        /// 初始化进程信息方法，由于利用了WMI查询，可能会有一些效率问题，目前还未找到其他好方法。
        /// 解决方案可以考虑使用多线程，一边加载一边显示，或者第一屏不显示加载的进程而显示其他信息来减缓效率问题导致的使用延迟问题。
        /// </summary>
        /// <returns>返回一个Dictionary对象.</returns>
        static private Dictionary<int, ProcessInfo> GetProcessesStateInfo()
        {
            processes = new Dictionary<int, ProcessInfo>();
            foreach (Process process in Process.GetProcesses())
            {
                processes.Add(process.Id, new ProcessInfo(process));
            }
            return processes;
        }

        /// <summary>
        /// 进程状态刷新方法。
        /// 此方法会删除已经死掉的进程，添加新增加进程信息，刷新已经存在的进程的CPU和内存占用。
        /// </summary>
        /// <returns></returns>
        static public Dictionary<int, ProcessInfo> RefreshProcessStateInfo()
        {
            if (processes == null || processes.Count == 0)
            {
                return GetProcessesStateInfo();
            }
            var systemProcesses = Process.GetProcesses();
            var needDeleteProcessIdCollection = processes.Keys.ToList();
            foreach (Process systemProcess in systemProcesses)
            {
                if (processes.ContainsKey(systemProcess.Id))
                {
                    processes[systemProcess.Id].RefreshProcessInfo(systemProcess);
                }
                else
                {
                    processes.Add(systemProcess.Id, new ProcessInfo(systemProcess));
                }
                if (needDeleteProcessIdCollection.Exists(x => processes.ContainsKey(systemProcess.Id)))
                {
                    needDeleteProcessIdCollection.Remove(needDeleteProcessIdCollection.FindIndex(x => x == systemProcess.Id) + 1);
                }
            }
            foreach (int i in needDeleteProcessIdCollection)
            {
                processes.Remove(i);
            }
            return processes;

        }
        public class ProcessInfo
        {
            private int mId;
            private String mName;
            private float mCpuOccupancyRate;
            private String mLocalPath;
            private String mUser;
            private float mMemoryOccupancy;
            private String mDiscription;
            private ManagementObjectSearcher searcher;
            private ManagementObjectCollection processDataCache;

            public int Id { get { return this.mId; } }
            public String Name { get { return this.mName; } }
            public float CpuOccupancyRate
            {
                get { return this.mCpuOccupancyRate; }
                set { this.mCpuOccupancyRate = value; }
            }
            public String LocalPath { get { return this.mLocalPath; } }
            public String User { get { return this.mUser; } }
            public float MomoryOccupancy
            {
                get { return this.mMemoryOccupancy; }
                set { this.mMemoryOccupancy = value; }
            }
            public string Discription { get { return this.mDiscription; } }

            public ProcessInfo(Process process)
            {
                this.mId = process.Id;
                this.mName = process.ProcessName;
                searcher = new ManagementObjectSearcher("Select ExecutablePath,Caption From Win32_Process Where ProcessID = '" + this.mId + "'");
                processDataCache = searcher.Get();
                foreach (ManagementObject processDataObj in processDataCache)
                {
                    this.mLocalPath = (String)processDataObj["ExecutablePath"];
                    this.mDiscription = (String)processDataObj["Caption"];
                    this.mUser = GetProcessOwner(processDataObj);
                }
                this.mCpuOccupancyRate = GetCpuOccupancyRateByProcess(process);
                this.mMemoryOccupancy = GetMemoryOccupancyRateByProcess(process);
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
            public void RefreshProcessInfo(Process process)
            {
                this.mCpuOccupancyRate = this.GetCpuOccupancyRateByProcess(process);
                this.mMemoryOccupancy = this.GetMemoryOccupancyRateByProcess(process);
            }
            private float GetCpuOccupancyRateByProcess(Process process)
            {
                PerformanceCounter process_cpu = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                return process_cpu.NextValue();
            }
            private float GetMemoryOccupancyRateByProcess(Process process)
            {
                PerformanceCounter process_memory = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
                return process_memory.NextValue();
            }

        }


    }
}
