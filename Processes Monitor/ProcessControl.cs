using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes_Monitor
{
    class ProcessControl
    {

        public Exception ProcessException { public get; private set; }
        public bool KillProcess(Process process)
        {
            try
            {
                process.Kill();
                return true;
            }
                //for log
            catch (System.ComponentModel.Win32Exception win32Exception_Limit)
            {
                ProcessException = win32Exception_Limit;
                return false;
            }
            catch(System.NotSupportedException notSupportException)
            {
                ProcessException = notSupportException;
                return false;
            }
            catch(System.InvalidOperationException invalidOperationException_COM)
            {
                ProcessException = invalidOperationException_COM;
                return false;
            }
        }
        
        public ProcessThreadCollection GetProcessThread(Process process)
        {
            return process.Threads;
        }
    }
}
