using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Threading;

namespace Processes_Monitor
{
    public class ServiceControl :IDisposable
    {
        static public Exception ServiceException { get;private set; }

        static public ServiceController Controller{get;private set;}

        static private bool StopService()
        {
            
            if (Controller.CanStop)
            {
                try
                {
                    Controller.Stop();
                }
                catch(Exception e)
                {
                    ServiceException = e;
                    return false;
                }
                do { Thread.Sleep(333); }
                while (Controller.Status != ServiceControllerStatus.Stopped);
                return true;
            }
            ServiceException = new Exception("Service Can not stop now!");
            return false;
        }
        
        static private bool StartService()
        {
            if(Controller.Status==ServiceControllerStatus.Stopped)
            {
                try
                {
                    Controller.Start();
                }
                catch(Exception e)
                {
                    ServiceException = e;
                    return false;
                }
                do { Thread.Sleep(333); }
                while (Controller.Status != ServiceControllerStatus.Running);
                return true;   
            }
            ServiceException = new Exception("Service is still running, please stop it first or Restart service.");
            return false;
        }

        static private bool RestartService()
        {
            if(StopService())
            {
                if (StartService())
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        static public bool ControlService(Processes_Monitor.ServiceStateInfo.ServiceInfo serviceInfo, ServiceControlOption controlOption)
        {
            Controller = new ServiceController(serviceInfo.Name);
            bool result = false;
            Thread serviceControlThread = new Thread(()=>
            {
               switch(controlOption)
               {
                   case ServiceControlOption.Start:
                       result = StartService();
                       break;
                   case ServiceControlOption.Stop:
                       result = StopService();
                       break;
                   case ServiceControlOption.Restart:
                       result = RestartService();
                       break;
                   default:
                       ServiceException = new Exception("Control Option is wrong!");
                       break;
               }
            });
            serviceControlThread.Start();
            serviceControlThread.Join();
            serviceControlThread.Abort();
            return result;
        }
        

        #region IDisposable Members

        public void Dispose()
        {
            System.GC.Collect(0);
        }

        #endregion
    }
    public enum ServiceControlOption
    {
        Start = 0,
        Stop = 1,
        Restart = 2,
    }
}
