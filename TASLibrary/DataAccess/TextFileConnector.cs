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

        //string filePath = @"C:\Users\mhdb9\Documents\GitHub\Bus-Company\trips.txt";
        string filePath = @"C:\Users\Talha\source\repos\Bus-Company\trips.txt";
        //string filePath = $"{System.AppDomain.CurrentDomain.BaseDirectory}info.txt");

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

        public CLinkedList<TripModel> GetTrip_All()
        {
            CLinkedList<TripModel> t; 
            using (Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                t = (CLinkedList<TripModel>)formatter.Deserialize(fs);
            }

            return t;
        }

        public void Trip_InsertAll(CLinkedList<TripModel> trips)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, trips);
            }
        }
    }
}
