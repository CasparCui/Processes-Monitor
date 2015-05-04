using System;
using System.IO;
using System.Text;

namespace ProcessLogger
{
    public enum LogLevel
    {
        Error = 0,
        Warning = 1,
        Debug = 2,
        Info = 4,
    }

    public class Logger
    {
        private Logger()
        {
        }

        private static String outputPath = @"C:\logFile.txt";
        private static StreamWriter sWriter = null;

        public static void SetOutputPath(String path)
        {
            outputPath = path;
        }

        private static String CreateLogFile()
        {
            using (File.Create(outputPath)) { }
            return outputPath;
        }

        private static StreamWriter GetStreamForWriteLog()
        {
            if (sWriter == null)
            {
                return sWriter = new StreamWriter(File.Exists(outputPath) ? outputPath : CreateLogFile(), true, Encoding.UTF8);
            }
            else
            {
                return sWriter;
            }
        }

        private static void WriteLog(LogLevel level, String formatStr, params object[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    String s = "{" + i.ToString() + "}";
                    if (formatStr.IndexOf(s) >= 0)
                    {
                        formatStr = formatStr.Replace(s, args[i].ToString());
                    }
                }
                GetStreamForWriteLog().WriteLine("{0}:[{1}]{2}", level.ToString(), DateTime.Now.ToString(), formatStr);
            }
            catch (Exception e)
            {
                GetStreamForWriteLog().WriteLine("[{0}]{1}", DateTime.Now.ToString(), e.ToString());
            }
            finally
            {
                sWriter.Dispose();
                sWriter = null;
            }
        }

        public static void Error(string formatStr, params object[] args)
        {
            WriteLog(LogLevel.Error, formatStr, args);
        }

        public static void Warning(string formatStr, params object[] args)
        {
            WriteLog(LogLevel.Warning, formatStr, args);
        }

        public static void Debug(string formatStr, params object[] args)
        {
            WriteLog(LogLevel.Debug, formatStr, args);
        }

        public static void Info(string formatStr, params object[] args)
        {
            WriteLog(LogLevel.Info, formatStr, args);
        }

        public static void LoggerDispose()
        {
            sWriter.Dispose();
        }
    }
}