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

namespace TASLibrary.DataAccess
{
    public class TextFileConnector : IDataConnection
    {        
        string folderName = "TripsDB\\";
        string filesDirectory = "";

        public TextFileConnector()
        {
            filesDirectory = AppDomain.CurrentDomain.BaseDirectory + folderName;
            Directory.CreateDirectory(filesDirectory);
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

        public void Trip_InsertAll(CLinkedList<TripModel> trips)
        {
            string filePath = FilePathFinder(trips.First.Date);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, trips);               
            }
        }
        public string FilePathFinder(DateTime date)
        {
            string dateToPath = date.ToShortDateString();
            dateToPath = dateToPath.Replace('/', '-');
            string filePath = $"{filesDirectory}Trips List for {dateToPath}.txt";            
            return filePath;
        }   
        
        public int GetTripId()
        {
            int tripId;
            try
            {
                string[] data = File.ReadAllLines($"{filesDirectory}dbinfo.txt", Encoding.GetEncoding("iso-8859-9"));
                string[] info = data[0].Split(';');

                tripId = Convert.ToInt32(info[1]);

                return tripId;

            }
            catch (Exception)
            {

                UpdateTripId(1);
                return 1;
            }
        }

        public void UpdateTripId(int id)
        {
            File.WriteAllText($"{filesDirectory}dbinfo.txt", $"tripId;{id};");
        }
    }
}
