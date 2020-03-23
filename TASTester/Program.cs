using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASLibrary.Enums;

namespace TASTester
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> t = new LinkedList<string>();            
            CLinkedList<BusModel> Data = new CLinkedList<BusModel>();

            //Data.AddFirst(new BusModel("ASD1111", 50));
            //Data.AddFirst(new BusModel("ASD2222", 25));
            //Data.AddFirst(new BusModel("ASD3333", 30));
            //Data.AddFirst(new BusModel("ASD4444", 70));
            //Data.AddFirst(new BusModel("ASD5555", 80));
            //BusModel b = new BusModel("ASD1111", 50);
            //Data.AddLast(b);
            Data.AddLast(new BusModel("ASD1111", 50));
            Data.AddLast(new BusModel("ASD2222", 25));
            Data.AddLast(new BusModel("ASD3333", 30));
            //Data.RemoveLast();
            Data.AddLast(new BusModel("ASD4444", 70));
            Data.AddLast(new BusModel("ASD5555", 80));
            Console.WriteLine(Data.Remove(new BusModel("ASD1113", 50))); ;
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

            Console.ReadLine();
        }
    }
}
