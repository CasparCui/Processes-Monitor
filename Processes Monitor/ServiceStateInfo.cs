using System;
using System.Collections.Generic;
using System.Management;

namespace Processes_Monitor
{
    
    public enum ServiceRunningState
    {
        Stopped = 1,
        Start_Pending = 2,
        Stop_Pending = 3,
        Running = 4,
        Continue_Pending = 5,
        Pause_Pending = 6,
        Paused = 7,
        Unknown = 8,
    }
    /// <summary>
    /// 此类需要重写
    /// </summary>
    public class ServiceStateInfo
    {
        static private ManagementObjectCollection mCollection;
        static private ManagementObjectSearcher mSearcher;
        static private Dictionary<String, ServiceInfo> windowsService;
        static private String wmiSql = "Select Name, Description,State ,ProcessId From Win32_Service";

        static public Dictionary<String, ServiceInfo> WindowsServices
        {
            get
            {
                return GetWindowsService();
            }
        }

        static private Dictionary<String, ServiceInfo> GetWindowsService()
        {
            windowsService = new Dictionary<string, ServiceInfo>();
            mSearcher = new ManagementObjectSearcher(wmiSql);
            mCollection = mSearcher.Get();
            foreach (ManagementObject managerObj in mCollection)
            {
                windowsService.Add((String)managerObj["Name"], new ServiceInfo(managerObj));
            }
            return windowsService;
        }

        public class ServiceInfo
        {
            private String mDescription;
            private String mName;
            private uint? mProcessId;
            private ServiceRunningState mServiceState;
            public ServiceInfo(ManagementObject serviceObj)
            {
                this.mName = (String)serviceObj["Name"];
                this.mDescription = (String)serviceObj["Description"];
                this.mServiceState = (ServiceRunningState)Enum.Parse(typeof(ServiceRunningState), ((String)serviceObj["State"]).Replace(' ','_'));
                if (serviceObj["ProcessId"] != null)
                {
                    this.mProcessId = (uint)serviceObj["ProcessId"];
                }
                else
                {
                    mProcessId = null;
                }
            }

            public String Description { get { return this.mDescription; } }

            public String Name { get { return this.mName; } }
            public uint? ProcessID { get { return this.mProcessId; } }

            public ServiceRunningState ServiceState { get { return this.mServiceState; } set { this.mServiceState = value; } }
        }
    }
}