using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Processes_Monitor
{
    public class ProcessControl
    {
        public Exception ProcessException { get; private set; }

        public ProcessThreadCollection GetProcessThread(Process process)
        {
            return process.Threads;
        }

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
            catch (System.NotSupportedException notSupportException)
            {
                ProcessException = notSupportException;
                return false;
            }
            catch (System.InvalidOperationException invalidOperationException_COM)
            {
                ProcessException = invalidOperationException_COM;
                return false;
            }
        }
        public bool KillProcess(int processId)
        {

            var process = Process.GetProcessById(processId);
            return this.KillProcess(process);

        }
        /// <summary>
        /// 获取进程占用文件的路径集合 此方法需要配合handle.exe使用，handle.exe已经打包到工程中。
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>

        /// <summary>
        /// 打开process 占用文件路径的monitor，这个monitor是一个ListView控件。 这个功能为扩展功能。
        /// </summary>
        /// <param name="process"></param>
        public void OpenProcessOccupancyFilePathMonitor(Process process)
        {
        }

        /// <summary>
        /// 打开process 占用内存的monitor，这个monitor是一个Monitor控件。这个窗口需要集成内存分析功能。
        /// </summary>
        /// <param name="process"></param>
        public void OpenProcessWorkingSetMemoryMonitor(Process process)
        {
        }

        private List<String> GetProcessOccupancyFilePath(int processId)
        {
            List<String> filePathList = new List<string>();
            Process cmd = new Process(); //获得系统信息，使用的是 handle.exe 这个控制台程序
            cmd.StartInfo.FileName = "handle.exe"; //将cmd的标准输入和输出全部重定向到.NET的程序里
            cmd.StartInfo.UseShellExecute = false; //此处必须为false否则引发异常
            cmd.StartInfo.RedirectStandardInput = true; //标准输入
            cmd.StartInfo.RedirectStandardOutput = true; //标准输出
            cmd.StartInfo.CreateNoWindow = true; //不显示命令行窗口界面
            cmd.StartInfo.Arguments = "-p " + processId.ToString(); //设定参数
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //显示windows 风格为隐藏
            cmd.Start(); //启动进程
            var result = cmd.StandardOutput.ReadToEnd();//获取输出
            cmd.WaitForExit();//等待控制台程序执行完成
            cmd.Close();//关闭该进程

            //处理获取到的结果，只考虑有路径的结果，其中有可能有文件夹路径
            var resultArrayTemp = result.Split('\n');
            foreach (String tempString in resultArrayTemp)
            {
                if (tempString.IndexOf(":\\") > 1)
                {
                    filePathList.Add(tempString.Substring(tempString.IndexOf(":\\") - 1).Trim('\r'));
                }
            }
            return filePathList;
        }
    }
}