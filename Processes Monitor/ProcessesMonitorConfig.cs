using System;
using System.Collections.Generic;
using System.Xml;

namespace Processes_Monitor
{
    public class ProcessesMonitorConfig
    {
        static private String destinationFolderPath;
        private static List<String> filePath;

        private static XmlNode mXmlRootNode;

        static public String DestinationFolderPath
        {
            get
            {
                if (String.IsNullOrEmpty(destinationFolderPath))
                {
                    destinationFolderPath = (xmlRootNode.SelectSingleNode("destinationPath") as XmlElement).GetAttribute("FolderPath").ToString().Trim('\\');
                }
                return destinationFolderPath;
            }
        }

        public static List<String> FilePath
        {
            get
            {
                if (filePath == null)
                {
                    filePath = GetFilesPathFromXML();
                }
                return filePath;
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

        static public List<String> GetFilesPathFromXML()
        {
            var tempPathList = new List<String>();
            var filesNode = xmlRootNode.SelectSingleNode("SourceFiles");
            var fileNameNodes = filesNode.SelectNodes("File");
            foreach (XmlNode node in fileNameNodes)
            {
                tempPathList.Add((filesNode as XmlElement).GetAttribute("FilePath").ToString().Trim('\\') + "\\" + (node as XmlElement).GetAttribute("FileName").Trim('\\'));
            }
            return tempPathList;
        }
    }
}