using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Processes_Monitor
{
    public class FileControl
    {
        public FileCollection FileCollection { get; private set; }
        private String destinationFolderPath;
        private ProcessControl processControler;
        private bool IsDestinationFiles;
        public List<Exception> FileControlException { get; private set; }
        public FileControl(FileCollection files)
        {
            this.FileCollection = files;
            destinationFolderPath = ProcessesMonitorConfig.DestinationFolderPath;
            processControler = new ProcessControl();
            FileControlException = new List<Exception>();
            if (FileCollection.Files.Count > 0)
            {
                if (FileCollection.Files[0].FullName.Contains(destinationFolderPath))
                {
                    IsDestinationFiles = true;
                }
                else
                {
                    IsDestinationFiles = false;
                }
            }
        }
        public bool StopFileOccupanciedProcesses()
        {
            if (IsDestinationFiles)
            {
                try
                {
                    var processes = FileCollection.OccupancyProcesses;
                    foreach (var process in processes.Values)
                    {
                        if (process is Process)
                        {
                            processControler.KillProcess(process);
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    FileControlException.Add(e);
                }
                
            }
            return false;
        }

        public bool RestartStopedService()
        {
            if(IsDestinationFiles)
            {
                try
                {
                    var services = FileCollection.OccupancyServices;
                    foreach( var service in services.Values)
                    {
                        if(service is ServiceStateInfo.ServiceInfo)
                        {
                            ServiceControl.ControlService(service, ServiceControlOption.Start);
                        }
                    }
                    return true;
                }
                catch(Exception e)
                {
                    FileControlException.Add(e);
                }
            }
            return false;
        }
        public bool CopyToDestinationFolder(bool OccupanciedProcessesHaveBeenKilled)
        {
            if(OccupanciedProcessesHaveBeenKilled && !this.IsDestinationFiles)
            {
                foreach(var fileInfo in FileCollection.Files)
                {
                    try
                    {
                        fileInfo.CopyTo(this.destinationFolderPath,true);
                    }
                    catch (IOException ioException)
                    {
                        this.FileControlException.Add(ioException);
                    }
                    catch(Exception e)
                    {
                        this.FileControlException.Add(e);
                    }
                }
                return true;
            }
            return false;
        }

    }
}
