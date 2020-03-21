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
            //Connection = new TextFileConnector();
            Connection = new PseudoConnector();
        }
    }
}
