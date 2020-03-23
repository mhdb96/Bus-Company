using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    public class TripModel
    {
        public int No { get; set; }
        public DestinationModel Destination { get; set; }
        public DateTime Date { get; set; }
        public BusModel Bus { get; set; }
        public DriverModel Driver { get; set; }
        public decimal SeatPrice { get; set; }
        //public int Seats { get; set; }

        public CLinkedList<TripModel> z { get; set; }
        public void XMLKaydet()
        {
            var xml = new XmlSerializer(typeof(TripModel));
            using (StreamWriter sw = new StreamWriter(@"C:\\Users\\Talha\\source\\repos\\Bus-Company\\trips.txt"))
            {
                xml.Serialize(sw, this);
            }
        }
    }
}
