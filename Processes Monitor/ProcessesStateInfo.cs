using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Processes_Monitor
{
    public class ProcessesStateInfo
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
        /// 进程状态刷新方法。
        /// 此方法会删除已经死掉的进程，添加新增加进程信息，刷新已经存在的进程的CPU和内存占用。
        /// </summary>
        /// <returns>返回一个Dictionary对象.Key为Process Id，Value 为ProcessInfo对象</returns>
        static private Dictionary<int, ProcessInfo> RefreshProcessStateInfo()
        {
            if (processes == null || processes.Count == 0)
            {
                return GetProcessesStateInfo();
            }
            var systemProcesses = Process.GetProcesses();
            Dictionary<int, Process> systemProcessesDictionary = new Dictionary<int, Process>();
            List<int> needDeleteKey = new List<int>();
            foreach (Process systemProcess in systemProcesses)
            {
                systemProcessesDictionary.Add(systemProcess.Id, systemProcess);
                if (processes.ContainsKey(systemProcess.Id))
                {
                    processes[systemProcess.Id].RefreshProcessInfo(systemProcess);
                }
                else
                {
                    processes.Add(systemProcess.Id, new ProcessInfo(systemProcess));
                }
            }
            foreach(var process in processes)
            {
                if(!systemProcessesDictionary.ContainsKey(process.Key))
                {
                    needDeleteKey.Add(process.Key);
                }
            }
            foreach(var key in needDeleteKey)
            {
                processes.Remove(key);
            }
            return processes;
        }

        /// <summary>
        /// 初始化进程信息方法，由于利用了WMI查询，可能会有一些效率问题，目前还未找到其他好方法。
        /// 解决方案可以考虑使用多线程，一边加载一边显示，或者第一屏不显示加载的进程而显示其他信息来减缓效率问题导致的使用延迟问题。【已解决】
        /// 2015-04-16 优化了调用算法，效率提升90%。
        /// </summary>
        /// <returns>返回一个Dictionary对象.Key为Process Id，Value 为ProcessInfo对象</returns>
        static private Dictionary<int, ProcessInfo> GetProcessesStateInfo()
        {
            processes = new Dictionary<int, ProcessInfo>();

            var searcher = new ManagementObjectSearcher("Select * From Win32_Process");
            var processDataCache = searcher.Get();
            foreach (ManagementObject processObj in processDataCache)
            {
                processes.Add(Convert.ToInt32(processObj["ProcessId"]), new ProcessInfo(processObj));
            }
            return processes;
        }
        public class ProcessInfo
        {
            private float mCpuOccupancyRate;
            private String mDiscription;
            private int mId;
            private String mLocalPath;
            private float mMemoryOccupancy;
            private String mName;
            private String mUser;
            private ManagementObjectCollection processDataCache;
            private ManagementObjectSearcher searcher;
            /// <summary>
            /// 添加单个process 信息专用构造方法。
            /// </summary>
            /// <param name="process"></param>
            public ProcessInfo(Process process)
            {
                this.Process = process;
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
                this.mCpuOccupancyRate = GetCpuOccupancyRateByProcess(mName);
                this.mMemoryOccupancy = GetMemoryOccupancyRateByProcess(mName);
            }

            /// <summary>
            /// 2015-04-16 添加了新的构造方法，此方法直接load WMI数据只有一次，执行效率大幅提升。同时原有构造方法保留，专用于更新单个process。
            /// </summary>
            /// <param name="processDataObj"></param>
            public ProcessInfo(ManagementObject processDataObj)
            {
                this.mId = Convert.ToInt32(processDataObj["ProcessId"]);
                this.mName = (String)processDataObj["Name"];
                try
                {
                    this.mLocalPath = (String)processDataObj["ExecutablePath"];
                }
                catch
                {
                    this.mLocalPath = string.Empty;
                }
                this.mDiscription = (String)processDataObj["Caption"];
                try
                {
                    object[] ownerInfo = new object[2];
                    uint resultNumber = (uint)processDataObj.InvokeMethod("GetOwner", ownerInfo);
                    if (ownerInfo[0]!=null&&ownerInfo[1]!=null)
                    {
                        this.mUser = String.Format("{0}\\{1}", (String)ownerInfo[0], (String)ownerInfo[1]);
                    }
                    else
                    {
                        this.mUser = "SYSTEM";
                    }
                }
                catch
                {
                    this.mUser = "SYSTEM";
                }
                this.mCpuOccupancyRate = GetCpuOccupancyRateByProcess(mName);
                this.mMemoryOccupancy = GetMemoryOccupancyRateByProcess(mName);
            }

            public float CpuOccupancyRate
            {
                get { return this.mCpuOccupancyRate; }
                set { this.mCpuOccupancyRate = value; }
            }

            public string Discription { get { return this.mDiscription; } }

            public int Id { get { return this.mId; } }

            public String LocalPath { get { return this.mLocalPath; } }

            public float MemoryOccupancy
            {
                get { return this.mMemoryOccupancy; }
                set { this.mMemoryOccupancy = value; }
            }

            public String Name { get { return this.mName; } }

            public Process Process { get; private set; }
            public String User { get { return this.mUser; } }
            public void RefreshProcessInfo(Process process)
            {
                this.mCpuOccupancyRate = this.GetCpuOccupancyRateByProcess(process.ProcessName);
                this.mMemoryOccupancy = this.GetMemoryOccupancyRateByProcess(process.ProcessName);
            }

            private float GetCpuOccupancyRateByProcess(String processName)
            {
                PerformanceCounter process_cpu = new PerformanceCounter("Process", "% Processor Time", processName);
                try
                {
                    float cpu = process_cpu.NextValue();
                    return cpu;
                }
                catch
                {
                    return 0;
                }
            }

            private float GetMemoryOccupancyRateByProcess(String processName)
            {
                PerformanceCounter process_memory = new PerformanceCounter("Process", "Working Set - Private", processName);
                try
                {
                    return process_memory.NextValue()/(1024*1024);
                }
                catch
                {
                    return 0;
                }
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

            public float RefreshProcessMemory()
            {
                this.MemoryOccupancy = this.GetMemoryOccupancyRateByProcess(this.Name);
                return this.MemoryOccupancy;
            }
        }
    }
}