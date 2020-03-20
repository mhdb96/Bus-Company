using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    public class BusModel
    {
        public string Plate { get; set; }
        public int Capacity { get; set; }

        public BusModel ()
        {

        }

        public BusModel(string plate, int capacity)
        {
            Plate = plate;
            Capacity = capacity;
        }

        public static CLinkedList<BusModel> GetSampleData()
        {
            CLinkedList<BusModel> Data = new CLinkedList<BusModel>();
            Data.Add(new BusModel("ASD1111", 50));
            Data.Add(new BusModel("ASD2222", 25));
            Data.Add(new BusModel("ASD3333", 30));
            Data.Add(new BusModel("ASD4444", 70));
            Data.Add(new BusModel("ASD5555", 80));
            return Data;
        }
    }
}
