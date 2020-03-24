using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using TASLibrary.CustomDataStructures;
using System.Runtime.Serialization;

namespace TASLibrary.Models
{
    [Serializable]
    public class TripModel : IEquatable<TripModel>, ISerializable
    {
        public int No { get; set; }
        public DestinationModel Destination { get; set; } = new DestinationModel();
        public DateTime Date { get; set; }
        public BusModel Bus { get; set; } = new BusModel();
        public DriverModel Driver { get; set; } = new DriverModel();
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

        public override int GetHashCode()
        {
            int hashCode = -1098416267;
            hashCode = hashCode * -1521134295 + No.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DestinationModel>.Default.GetHashCode(Destination);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BusModel>.Default.GetHashCode(Bus);
            hashCode = hashCode * -1521134295 + EqualityComparer<DriverModel>.Default.GetHashCode(Driver);
            hashCode = hashCode * -1521134295 + SeatPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<CLinkedList<SeatModel>>.Default.GetHashCode(Seats);            
            return hashCode;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("No", No);
            info.AddValue("Destination", Destination);
            info.AddValue("Date", Date);
            info.AddValue("Bus", Bus);
            info.AddValue("Driver", Driver);
            info.AddValue("SeatPrice", SeatPrice);
            info.AddValue("Seats", Seats);
        }

        public TripModel(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            No = (int)info.GetValue("No", typeof(int));
            Destination = (DestinationModel)info.GetValue("Destination", typeof(DestinationModel));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            Bus = (BusModel)info.GetValue("Bus", typeof(BusModel));
            Driver = (DriverModel)info.GetValue("Driver", typeof(DriverModel));
            SeatPrice = (decimal)info.GetValue("SeatPrice", typeof(decimal));
            Seats = (CLinkedList<SeatModel>)info.GetValue("Seats", typeof(CLinkedList<SeatModel>));
        }
        public TripModel()
        {

        }
    }
}
