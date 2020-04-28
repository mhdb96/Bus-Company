using TASLibrary.DataAccess;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace TASUI
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }
        public static FileDbInfo info { get; set; }
        public static void InitializeConnections()
        {
            FileDbInfoBuilder builder = new FileDbInfoBuilder();
            info = builder
                .ProjectName("hamada")
                .LogFolder("loooogs")
                .TextFolder("text")
                .DbFolder("mydms")
                .Directory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\")
                .GetFileInfo();
            Connection = new TextFileConnector(info == null ? info = new FileDbInfo() : info);
        }
    }
}
