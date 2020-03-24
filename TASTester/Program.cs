using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASLibrary.Enums;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TASTester
{
    class Program
    {
        //static string filepath = @"C:\Users\mhdb9\Documents\GitHub\Bus-Company\trips.txt";
        static string filepath = @"C:\Users\Talha\source\repos\Bus-Company\trips.txt";
        static void Main(string[] args)
        {
            //ListTesting();
            XmlTesting();

            Console.ReadLine();
        }

        static private void ListTesting()
        {
            LinkedList<string> t = new LinkedList<string>();
            CLinkedList<BusModel> Data = new CLinkedList<BusModel>();
            CLinkedList<BusModel> data = new CLinkedList<BusModel>();
            //Data.AddFirst(new BusModel("ASD1111", 50));
            //Data.AddFirst(new BusModel("ASD2222", 25));
            //Data.AddFirst(new BusModel("ASD3333", 30));
            //Data.AddFirst(new BusModel("ASD4444", 70));
            //Data.AddFirst(new BusModel("ASD5555", 80));
            BusModel b = new BusModel();
            TripModel tr = new TripModel();
            DestinationModel d = new DestinationModel("r");
            PassengerModel p = new PassengerModel();
            p.Sex = SexType.Female;
            Console.WriteLine(p.Sex.ToString());
            p.GetHashCode();
            d.Name = "tt";
            d.GetHashCode();
            //Data.AddLast(b);
            Data.AddLast(new BusModel("ASD1111", 50));
            Data.AddLast(new BusModel("ASD2222", 50));
            Data.AddLast(new BusModel("ASD3333", 30));
            //Data.RemoveLast();
            Data.AddLast(new BusModel("ASD4444", 70));
            Data.AddLast(new BusModel("ASD5555", 80));

            data.AddLast(new BusModel("ASD1111", 50));
            data.AddLast(new BusModel("ASD2222", 50));
            data.AddLast(new BusModel("ASD3333", 30));
            //Data.RemoveLast();
            data.AddLast(new BusModel("ASD4444", 70));
            data.AddLast(new BusModel("ASD5555", 80));
            bool h = data.GetHashCode() == Data.GetHashCode();
            //Data.GetHashCode();
            Console.WriteLine(Data.Find(x => x.Capacity == 80).Plate);
            //Data.RemoveFirst();
            //Data.AddFirst(new BusModel("ASD0000", 80));
            //Console.WriteLine(Data.Last.Plate);
            //Data.RemoveAt(4);
            for (int i = 0; i < Data.Count; i++)
            {
                Console.WriteLine(Data[i].Plate);
            }
            //Console.WriteLine(Data[1].Plate); 
            //CLinkedList<BusModel> buses = BusModel.GetSampleData();            
            //foreach (BusModel model in Data)
            //{
            //    Console.WriteLine(model.Plate);
            //}
            //Console.WriteLine(Data.Last.Plate);
            //CLinkedList<DestinationModel> dests = DestinationModel.GetSampleData();
            //CNode<DestinationModel> dest = dests.First;
            //while (dest != null)
            //{
            //    Console.WriteLine(dest.Data.Name);
            //    dest = dest.Next;
            //}

            //CLinkedList<DriverModel> drivers = DriverModel.GetSampleData();
            //CNode<DriverModel> driver = drivers.First;
            //while (driver != null)
            //{
            //    Console.WriteLine(driver.Data.Name);
            //    driver = driver.Next;
            //}
        }
        static private void XmlTesting()
        {
            CLinkedList<TripModel> trips = new CLinkedList<TripModel>();
            TripModel model = new TripModel();
            model.No = 22;
            model.Destination.Name = "Kocaeli";
            model.Bus.Capacity = 10;
            model.Bus.Plate = "ASD1234";
            model.Driver.Name = "Ahmet";
            model.Date = DateTime.Now;
            model.Seats.AddLast(new SeatModel(1, new PassengerModel("muhammed", SexType.Male), SeatStatus.Sold));
            TripModel test = new TripModel();
            test.No = 22;
            test.Destination.Name = "Locaeli";
            test.Bus.Capacity = 17;
            test.Bus.Plate = "ASD1234";
            test.Driver.Name = "Ahmet";
            test.Date = DateTime.Now;
            test.Seats.AddLast(new SeatModel(1, new PassengerModel("Ahmad", SexType.Male), SeatStatus.Sold));
            trips.AddLast(model);
            trips.AddLast(test);
            //Create(trips);
            // trips = load();

            IFormatter formatter = new BinaryFormatter();
            SerializeItem(filepath, formatter, trips); // Serialize an instance of the class.
            DeserializeItem(filepath, formatter); // Deserialize the instance.
            Console.WriteLine("Done");
            Console.ReadLine();

        }
        public static void SerializeItem(string fileName, IFormatter formatter, CLinkedList<TripModel> model)
        {
            FileStream s = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(s, model);
            s.Close();
        }

        public static void DeserializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            CLinkedList<TripModel> t = (CLinkedList<TripModel>)formatter.Deserialize(s);

            //Console.WriteLine(t.No);
            //Console.WriteLine(t.Destination.Name);
            //Console.WriteLine(t.Date);
            //Console.WriteLine(t.Bus.Capacity);
            //Console.WriteLine(t.Driver.Name);
            //Console.WriteLine(t.SeatPrice);
            //Console.WriteLine(t.Seats[0].Passenger.Name);

        }
        public static void Create(CLinkedList<TripModel>  model)
        {
            using (Stream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CLinkedList<TripModel>));
                serializer.Serialize(fs, model);
            }
        }
        public static CLinkedList<TripModel> load()
        {
            // Delete list data
            CLinkedList<TripModel> model;

            // Read data from XML
            XmlSerializer serializer = new XmlSerializer(typeof(CLinkedList<TripModel>));

            using (FileStream fs = File.OpenRead(filepath))
            {
                model = (CLinkedList<TripModel>)serializer.Deserialize(fs);
            }
            return model;
        }

    }
}
