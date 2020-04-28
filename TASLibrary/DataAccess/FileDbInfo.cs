using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.DataAccess
{
    public class FileDbInfo
    {
        private string dbFolderName;
        private string textFolderName;
        private string logFolderName;
        private string projectDirectory;
        private string projectFolderName;
        private string dbInfoFileName;

        public FileDbInfo()
        {
            dbFolderName = "DbFiles";
            textFolderName = "";
            logFolderName = "Logs";
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ProjectFolderName = "TripsDB";
        }

        public string DbFolderName { get => dbFolderName; set => dbFolderName = value; }
        public string TextFolderName { get => textFolderName; set => textFolderName = value; }
        public string LogFolderName { get => logFolderName; set => logFolderName = value; }
        public string ProjectDirectory { get => $"{projectDirectory}{projectFolderName}\\"; set => projectDirectory = value; }
        public string ProjectFolderName { get => projectFolderName; set => projectFolderName = value; }
        public string DbInfoFileName { get => dbInfoFileName; set => dbInfoFileName = value; }
    }
    public class FileDbInfoBuilder
    {
        FileDbInfo fileDbInfo = new FileDbInfo();
        public FileDbInfoBuilder DbFolder(string folderName)
        {
            fileDbInfo.DbFolderName = folderName;
            return this;
        }
        public FileDbInfoBuilder TextFolder(string folderName)
        {
            fileDbInfo.TextFolderName = folderName;
            return this;
        }
        public FileDbInfoBuilder LogFolder(string folderName)
        {
            fileDbInfo.LogFolderName = folderName;
            return this;
        }
        public FileDbInfoBuilder Directory(string folderName)
        {
            fileDbInfo.ProjectDirectory = folderName;
            return this;
        }
        public FileDbInfoBuilder ProjectName(string folderName)
        {
            fileDbInfo.ProjectFolderName = folderName;
            return this;
        }
        public FileDbInfo GetFileInfo()
        {
            return fileDbInfo;
        }

    }
}
