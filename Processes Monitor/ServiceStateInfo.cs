using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Processes_Monitor
{
    public class ServiceStateInfo
    {
        static private Dictionary<String, ServiceInfo> windowsService;
        static private ManagementObjectSearcher mSearcher;
        static private ManagementObjectCollection mCollection;
        static private String wmiSql = "Select Name, Description,State ,ProcessId From Win32_Service";

        static public Dictionary<String, ServiceInfo> WindowsService
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
            private String mName;
            private String mDescription;
            private ServiceRunningState mServiceState;
            private uint? mProcessId;

            public String Name { get { return this.mName; } }
            public String Description { get { return this.mDescription; } }
            public ServiceRunningState ServiceState { get { return this.mServiceState; } set { this.mServiceState = value; } }
            public uint? ProcessID { get { return this.mProcessId; } }

            public ServiceInfo(ManagementObject serviceObj)
            {
                this.mName = (String)serviceObj["Name"];
                this.mDescription = (String)serviceObj["Description"];
                this.mServiceState = (ServiceRunningState)Enum.Parse(typeof(ServiceRunningState), (String)serviceObj["State"]);
                if (serviceObj["ProcessId"] != null)
                {
                    this.mProcessId = (uint)serviceObj["ProcessId"];
                }
                else
                {
                    mProcessId = null;
                }
            }
        }

    }
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
}
