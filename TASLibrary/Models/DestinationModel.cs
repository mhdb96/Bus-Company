using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    public class DestinationModel
    {
        public string Name { get; set; }

        public DestinationModel()
        {

        }

        public DestinationModel(string name)
        {
            Name = name;
        }

        public static CLinkedList<DestinationModel> GetSampleData()
        {
            CLinkedList<DestinationModel> Data = new CLinkedList<DestinationModel>();
            Data.Add(new DestinationModel("Kocaeli - Ankara"));
            Data.Add(new DestinationModel("Kocaeli - Izmir"));
            Data.Add(new DestinationModel("Kocaeli - Istanbul"));                        
            return Data;
        }
    }
}
