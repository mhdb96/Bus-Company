using TASLibrary.DataAccess;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace TASLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections()
        {
            FileDbInfoBuilder builder = new FileDbInfoBuilder();
            FileDbInfo info = builder.ProjectName("hamada")
                .LogFolder("loooogs")
                .TextFolder("text")
                .DbFolder("mydms")
                .Directory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\")
                .GetFileInfo();
            
            Connection = new TextFileConnector(info);
            //Connection = new PseudoConnector();
        }
    }
}
