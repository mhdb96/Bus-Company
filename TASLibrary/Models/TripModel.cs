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
    public class TripModel : IEquatable<TripModel>
    {
        public int No { get; set; }
        public DestinationModel Destination { get; set; }
        public DateTime Date { get; set; }
        public BusModel Bus { get; set; }
        public DriverModel Driver { get; set; }
        public decimal SeatPrice { get; set; }
        public CLinkedList<SeatModel> Seats { get; set; } = new CLinkedList<SeatModel>();

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TripModel);
        }
        public bool Equals(TripModel other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (this.GetType() != other.GetType())
            {
                return false;
            }
            return (No == other.No && Destination == other.Destination && Date == other.Date && Bus == other.Bus && Driver == other.Driver && SeatPrice == other.SeatPrice && Seats == other.Seats);
        }

        public static bool operator ==(TripModel lhs, TripModel rhs)
        {

            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }
        public static bool operator !=(TripModel lhs, TripModel rhs)
        {
            return !(lhs == rhs);
        }

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
