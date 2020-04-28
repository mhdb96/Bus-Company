using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASLibrary.Enums;

namespace TASLibrary.DataAccess
{
    public class TextFileConnector : IDataConnection
    {   
        private readonly string dbFilesDirectory;
        private readonly string textFilesDirectory;
        private readonly string logFilesDirectory;
        private readonly string dbInfoPath;        

        public Logger Logger { get; set; }

        public TextFileConnector(FileDbInfo info)
        {            
            dbFilesDirectory = $"{info.ProjectDirectory}{info.DbFolderName}\\";
            textFilesDirectory = $"{info.ProjectDirectory}{info.TextFolderName}\\";
            logFilesDirectory = $"{info.ProjectDirectory}{info.LogFolderName}\\";
            Directory.CreateDirectory(dbFilesDirectory);
            Directory.CreateDirectory(textFilesDirectory);
            Directory.CreateDirectory(logFilesDirectory);
            dbInfoPath = $"{dbFilesDirectory}dbinfo.info";
            Logger = new Logger(logFilesDirectory);
        }
        public CLinkedList<BusModel> GetBus_All()
        {
            return BusModel.GetSampleData();
        }

        public CLinkedList<DestinationModel> GetDestination_All()
        {
            return DestinationModel.GetSampleData();
        }

        public CLinkedList<DriverModel> GetDriver_All()
        {
            return DriverModel.GetSampleData();
        }

        public CLinkedList<TripModel> GetTrip_All(DateTime date)
        {
            CLinkedList<TripModel> t;
            string filePath = FileHelper.FilePathFinder(date, dbFilesDirectory, "dat");
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    t = (CLinkedList<TripModel>)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                t = new CLinkedList<TripModel>();                
            }

            return t;
        }

        public void Trip_InsertAll(CLinkedList<TripModel> trips, DateTime selectedDate)
        {
            string filePath = FileHelper.FilePathFinder(selectedDate, dbFilesDirectory, "dat");
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, trips);               
            }
            filePath = FileHelper.FilePathFinder(selectedDate, textFilesDirectory, "txt");
            File.WriteAllText(filePath, trips.ToString("Trips"), Encoding.GetEncoding("iso-8859-9"));
        }

        public void UpdateDbInfo(DbInfo info, int number)
        {
            CheckDbInfo();
            string[] data = File.ReadAllLines(dbInfoPath, Encoding.GetEncoding("iso-8859-9"));                       
            switch (info)
            {
                case DbInfo.TripId:
                    {
                        data[0] = $"Trip Id;{number};";
                        break;
                    }                    
                case DbInfo.TripCount:
                    {
                        data[1] = $"Trip Count;{number};";
                        break;
                    }                                    
            }
            File.WriteAllLines(dbInfoPath, data, Encoding.GetEncoding("iso-8859-9"));
        }
        public int GetDbInfo(DbInfo info)
        {
            CheckDbInfo();
            string[] data = File.ReadAllLines(dbInfoPath, Encoding.GetEncoding("iso-8859-9"));
            switch (info)
            {
                case DbInfo.TripId:
                    {
                        string[] line = data[0].Split(';');
                        int tripId = Convert.ToInt32(line[1]);
                        return tripId;                        
                    }

                case DbInfo.TripCount:
                    {
                        string[] line = data[1].Split(';');
                        int tripCount = Convert.ToInt32(line[1]);
                        return tripCount;
                    }
            }
            return -1;
        }
        private void CheckDbInfo()
        {
            if(!File.Exists(dbInfoPath))
            {
                string[] data = new string[2];
                data[0] = "Trip Id;0;";
                data[1] = "Trip Count;0;";
                File.WriteAllLines(dbInfoPath, data, Encoding.GetEncoding("iso-8859-9"));

                List<string> log = new List<string>();
                log.Add("dbinfo.info file created.");
                Logger.WriteLogsToFile(log);
            }
        }
    }
}
