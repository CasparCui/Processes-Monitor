using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Processes_Monitor
{
    public class FileCollection
    {
        private Dictionary<int, Process> occupancyProcesses;
        private Dictionary<int, ServiceStateInfo.ServiceInfo> occupancyService;

        public FileCollection(List<String> filesPath)
        {
            Files = new List<FileInfo>();
            CanNotGetFilesPath = new Dictionary<string, Exception>();
            foreach (var filePath in filesPath)
            {
                try
                {
                    Files.Add(new FileInfo(filePath));
                }
                catch (Exception e)
                {
                    CanNotGetFilesPath.Add(filePath, e);
                }
            }
        }

        public Dictionary<String, Exception> CanNotGetFilesPath { get; set; }

        public List<FileInfo> Files { get; set; }
        public Dictionary<int, Process> OccupancyProcesses
        {
            get
            {
                if (occupancyProcesses == null)
                {
                    occupancyProcesses = this.GetOccupancyProcesses();
                }
                return occupancyProcesses;
            }
        }
        public Dictionary<int, ServiceStateInfo.ServiceInfo> OccupancyServices
        {
            get
            {
                if (occupancyProcesses == null)
                {
                    occupancyProcesses = this.GetOccupancyProcesses();
                }
                if (occupancyService == null)
                {
                    occupancyService = this.GetOccupancyServices();
                }
                return occupancyService;

            }
        }
        private List<int> GetOccupancyProceeseId(String fileName)
        {
            var processesId = new List<int>();
            Process cmd = new Process(); //获得系统信息，使用的是 handle.exe 这个控制台程序
            cmd.StartInfo.FileName = "handle.exe"; //将cmd的标准输入和输出全部重定向到.NET的程序里
            cmd.StartInfo.UseShellExecute = false; //此处必须为false否则引发异常
            cmd.StartInfo.RedirectStandardInput = true; //标准输入
            cmd.StartInfo.RedirectStandardOutput = true; //标准输出
            cmd.StartInfo.CreateNoWindow = true; //不显示命令行窗口界面
            cmd.StartInfo.Arguments = fileName.ToString(); //设定参数
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //显示windows 风格为隐藏
            cmd.Start(); //启动进程
            var result = cmd.StandardOutput.ReadToEnd();//获取输出
            cmd.WaitForExit();//等待控制台程序执行完成
            cmd.Close();//关闭该进程

            var resultArray = result.Split('\n');
            foreach (var resultLine in resultArray)
            {
                if (resultLine.IndexOf("pid:") >= 0)
                {
                    processesId.Add(Convert.ToInt32(resultLine.Substring(resultLine.IndexOf("pid:", 6)).Trim(' ', '\t', 't')));
                }
            }
            return processesId;
        }

        private Dictionary<int, Process> GetOccupancyProcesses()
        {
            var resultOccupancyProcesses = new Dictionary<int, Process>();
            if (Files.Count > 0)
            {
                foreach (var file in Files)
                {
                    var processesId = this.GetOccupancyProceeseId(file.FullName);
                    foreach (var processId in processesId)
                    {
                        if (resultOccupancyProcesses.ContainsKey(processId))
                        {
                            continue;
                        }
                        else
                        {
                            resultOccupancyProcesses.Add(processId, Process.GetProcessById(processId));
                        }
                    }
                }
            }
            return resultOccupancyProcesses;
        }
        private Dictionary<int, ServiceStateInfo.ServiceInfo> GetOccupancyServices()
        {
            var resultOccupancyServices = new Dictionary<int, ServiceStateInfo.ServiceInfo>();
            if (this.occupancyProcesses != null && this.occupancyProcesses.Count >= 0)
            {
                var services = ServiceStateInfo.WindowsService;
                foreach (var process in occupancyProcesses)
                    foreach (var service in services)
                    {
                        if (process.Value.Id == service.Value.ProcessID)
                        {
                            resultOccupancyServices.Add(process.Value.Id, service.Value);
                        }
                    }
            }
            return resultOccupancyServices;
        }
    }
}