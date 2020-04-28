using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.DataAccess
{
    public class FileHelper
    {
        public static string FilePathFinder(DateTime date, string fileDirectory, string ext)
        {
            string dateToPath = date.ToShortDateString();
            dateToPath = dateToPath.Replace('/', '.');
            string filePath = $"{fileDirectory}{dateToPath}.{ext}";
            return filePath;
        }
    }
}
