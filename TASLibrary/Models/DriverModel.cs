using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    public class DriverModel
    {
        public string Name { get; set; }

        public DriverModel()
        {

        }

        public DriverModel(string name)
        {
            Name = name;
        }

        public static CLinkedList<DriverModel> GetSampleData()
        {
            CLinkedList<DriverModel> Data = new CLinkedList<DriverModel>();
            Data.AddLast(new DriverModel("Remzi Aslan"));
            Data.AddLast(new DriverModel("Can Hekimoglu"));
            Data.AddLast(new DriverModel("Ufuk Kantar"));
            Data.AddLast(new DriverModel("Furkan Korkmaz"));            
            return Data;
        }
    }
}
