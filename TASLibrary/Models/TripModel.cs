using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using TASLibrary.CustomDataStructures;
using System.Runtime.Serialization;
using TASLibrary.Enums;

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
        public string Time { 
            get
            {
                return Date.ToShortTimeString();
            }
        }
        public decimal Revenue { 
            get 
            {
                decimal sum = 0;
                foreach (var seat in Seats)
                {
                    if (seat.Status == SeatStatus.Sold)
                    {
                        sum += SeatPrice;
                    }
                }

                return sum;
            } 

            set { }
        }
        public CLinkedList<SeatModel> Seats { get; set; } = new CLinkedList<SeatModel>();

        public void CreateSeats()
        {
            Seats.Clear();
            for (int i = 0; i < Bus.Capacity; i++)
            {
                Seats.AddLast(new SeatModel(i + 1, new PassengerModel(), SeatStatus.Empty));
            }
        }

        public TripModel()
        {

        }

        public static CLinkedList<TripModel> GetSampleData()
        {
            CLinkedList<TripModel> trips = new CLinkedList<TripModel>();
            TripModel model = new TripModel();
            model.No = 21;
            model.Destination.Name = "Kocaeli - Ankara";
            model.Bus.Capacity = 50;
            model.Bus.Plate = "ASD1111";
            model.Driver.Name = "Remzi Aslan";
            model.Date = DateTime.Now;
            model.SeatPrice = 50;
            model.Seats.AddLast(new SeatModel(1, new PassengerModel("muhammed", SexType.Male), SeatStatus.Sold));
            model.Seats.AddLast(new SeatModel(1, new PassengerModel("fdgdfgdf", SexType.Male), SeatStatus.Sold));
            model.Seats.AddLast(new SeatModel(1, new PassengerModel("gfdgdfg", SexType.Male), SeatStatus.Sold));
            model.Seats.AddLast(new SeatModel(1, new PassengerModel("yhutyuty", SexType.Male), SeatStatus.Sold));
            TripModel test = new TripModel();
            test.No = 22;
            test.Destination.Name = "Kocaeli - Izmir";
            test.Bus.Capacity = 25;
            test.Bus.Plate = "ASD2222";
            test.Driver.Name = "Can Hekimoglu";
            test.Date = DateTime.Now;
            test.SeatPrice = 75;
            test.Seats.AddLast(new SeatModel(1, new PassengerModel("Ahmad", SexType.Male), SeatStatus.Sold));

            TripModel best = new TripModel();
            best.No = 23;
            best.Destination.Name = "Kocaeli - Istanbul";
            best.Bus.Capacity = 30;
            best.Bus.Plate = "ASD3333";
            best.Driver.Name = "Ufuk Kantar";
            best.Date = DateTime.Now;
            best.SeatPrice = 65;
            best.Seats.AddLast(new SeatModel(1, new PassengerModel("Ahmad", SexType.Male), SeatStatus.Sold));

            trips.AddLast(model);
            trips.AddLast(test);
            trips.AddLast(best);
            return trips;
        }

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
            
            if(Bus.Capacity == Seats.Count && Seats.Count != 0)
            {
                info.AddValue("Seats", Seats);
            }
            else
            {
                Seats.Clear();

                for (int i = 0; i < Bus.Capacity; i++)
                {
                    Seats.AddLast(new SeatModel(i+1, new PassengerModel(), SeatStatus.Empty));
                }
                info.AddValue("Seats", Seats);
            }            
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
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Trip's Information:");
            sb.AppendLine($"No: {No}");
            sb.AppendLine($"Seat Price: { SeatPrice}");                       
            sb.AppendLine(Destination.ToString());
            sb.AppendLine(Bus.ToString());
            sb.AppendLine(Driver.ToString());
            sb.AppendLine("");
            sb.Append(Seats.ToString("Seats"));            
            sb.AppendLine("******************************************************************************");
            return sb.ToString();
        }
    }
}
