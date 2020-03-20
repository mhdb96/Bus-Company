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
            CLinkedList<BusModel> buses = BusModel.GetSampleData();
            CNode<BusModel> bus = buses.header;
            while(bus != null)
            {
                Console.WriteLine(bus.Data.Plate);
                bus = bus.Next;
            }

            CLinkedList<DestinationModel> dests = DestinationModel.GetSampleData();
            CNode<DestinationModel> dest = dests.header;
            while (dest != null)
            {
                Console.WriteLine(dest.Data.Name);
                dest = dest.Next;
            }

            CLinkedList<DriverModel> drivers = DriverModel.GetSampleData();
            CNode<DriverModel> driver = drivers.header;
            while (driver != null)
            {
                Console.WriteLine(driver.Data.Name);
                driver = driver.Next;
            }

            Console.ReadLine();
        }
    }
}
