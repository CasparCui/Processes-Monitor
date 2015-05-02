using System;
using System.Collections.Generic;
using System.Xml;

namespace Processes_Monitor
{
    public enum FolderPathOption
    {
        Source = 0,
        Destination =1,
    }
    public class ProcessesMonitorConfig
    {
        static private String destinationFolderPath;
        static private String sourceFolderPath;
        private static List<String> sourceFilePath;
        private static List<String> destinationFilePath;

        private static XmlNode mXmlRootNode;

        static public String SourceFolderPath
        {
            get
            {
                if(String.IsNullOrEmpty(sourceFolderPath))
                {
                    sourceFolderPath = (xmlRootNode.SelectSingleNode("SourcePath") as XmlElement).GetAttribute("FolderPath").ToString();
                }
                return sourceFolderPath;
            }
        }
        static public String DestinationFolderPath
        {
            get
            {
                if (String.IsNullOrEmpty(destinationFolderPath))
                {
                    destinationFolderPath = (xmlRootNode.SelectSingleNode("DestinationPath") as XmlElement).GetAttribute("FolderPath").ToString().Trim('\\');
                }
                return destinationFolderPath;
            }
        }

        public static List<String> SourceFilePath
        {
            get
            {
                if (sourceFilePath == null)
                {
                    sourceFilePath = GetFilesPathFromXML(FolderPathOption.Source);
                }
                return sourceFilePath;
            }
        }
        public static List<String> DestinationFilePath
        {
            get
            {
                if(destinationFilePath == null)
                {
                    destinationFilePath = GetFilesPathFromXML(FolderPathOption.Destination);
                }
                return destinationFilePath;
            }
        }
        private static XmlNode xmlRootNode
        {
            get
            {
                if (mXmlRootNode == null)
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load("ProcessesMonitorConfig.config");
                    mXmlRootNode = xmlDoc.SelectSingleNode("Root");
                }
                return mXmlRootNode;
            }
            set
            {
                mXmlRootNode = value;
            }
        }

        static public List<String> GetFilesPathFromXML(FolderPathOption folder)
        {
            var tempPathList = new List<String>();
            var folderPath = String.Empty;
            switch(folder)
            {
                case FolderPathOption.Source:
                    folderPath = SourceFolderPath;
                    break;
                case FolderPathOption.Destination:
                    folderPath = DestinationFolderPath;
                    break;
                default:
                    throw new Exception("Error Input At Function GetFilesPathFormXml.");
            }
            var filesNode = xmlRootNode.SelectSingleNode("Files");
            var fileNameNodes = filesNode.SelectNodes("File");
            foreach (XmlNode node in fileNameNodes)
            {
                tempPathList.Add(folderPath.Trim('\\') + "\\" + (node as XmlElement).GetAttribute("FileName").Trim('\\'));
            }
            return tempPathList;
        }
    }
}