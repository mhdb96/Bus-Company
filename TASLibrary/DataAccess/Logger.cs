using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.DataAccess
{
    public class Logger
    {
        private string logFilesDirectory;

        public Logger(string logFilesDirectory)
        {
            this.logFilesDirectory = logFilesDirectory;
        }

        public void WriteLogsToFile(List<string> data)
        {
            string logFilePath = FileHelper.FilePathFinder(DateTime.Now, logFilesDirectory, "txt");
            File.AppendAllLines(logFilePath, data, Encoding.GetEncoding("iso-8859-9"));
        }
    }
}
