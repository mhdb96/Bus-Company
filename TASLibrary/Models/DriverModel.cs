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
            Data.Add(new DriverModel("Remzi Aslan"));
            Data.Add(new DriverModel("Can Hekimoglu"));
            Data.Add(new DriverModel("Ufuk Kantar"));
            Data.Add(new DriverModel("Furkan Korkmaz"));            
            return Data;
        }
    }
}
