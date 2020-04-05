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
    public class TextFileConnector //: IDataConnection
    {        
        const string dbFolderName = @"TripsDB\\DbFiles\\";
        const string textFolderName = @"TripsDB\\";
        const string logFolderName = @"TripsDB\\LogFiles\\";
        string dbFilesDirectory = "";
        string textFilesDirectory = "";
        string logFilesDirectory = "";
        string dbInfoPath = "";

        public TextFileConnector()
        {
            dbFilesDirectory = AppDomain.CurrentDomain.BaseDirectory + dbFolderName;
            textFilesDirectory = AppDomain.CurrentDomain.BaseDirectory + textFolderName;
            logFilesDirectory = AppDomain.CurrentDomain.BaseDirectory + logFolderName;
            Directory.CreateDirectory(dbFilesDirectory);
            Directory.CreateDirectory(textFilesDirectory);
            Directory.CreateDirectory(logFilesDirectory);
            dbInfoPath = $"{dbFilesDirectory}dbinfo.info";
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
            string filePath = FilePathFinder(date);
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    t = (CLinkedList<TripModel>)formatter.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                t = new CLinkedList<TripModel>();                
            }

            return t;
        }

        public void Trip_InsertAll(CLinkedList<TripModel> trips, DateTime selectedDate)
        {
            string filePath = FilePathFinder(selectedDate);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, trips);               
            }
            filePath = TextFilePathFinder(selectedDate);
            File.WriteAllText(filePath, trips.ToString("Trips"), Encoding.GetEncoding("iso-8859-9"));
        }
        public string LogFilePathFinder(DateTime date)
        {
            string dateToPath = date.ToShortDateString();
            dateToPath = dateToPath.Replace('/', '.');
            string filePath = $"{logFilesDirectory}logs - {dateToPath}.txt";
            return filePath;
        }
        public void WriteLogsToFile(List<string> data)
        {
            string logFilePath = LogFilePathFinder(DateTime.Now);
            File.AppendAllLines(logFilePath, data, Encoding.GetEncoding("iso-8859-9"));
        }
        public string FilePathFinder(DateTime date)
        {
            string dateToPath = date.ToShortDateString();
            dateToPath = dateToPath.Replace('/', '.');
            string filePath = $"{dbFilesDirectory}{dateToPath}.dat";            
            return filePath;
        }
        public string TextFilePathFinder(DateTime date)
        {
            string dateToPath = date.ToShortDateString();
            dateToPath = dateToPath.Replace('/', '.');
            string filePath = $"{textFilesDirectory}{dateToPath}.txt";
            return filePath;
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
                WriteLogsToFile(log);
            }
        }
    }
}
