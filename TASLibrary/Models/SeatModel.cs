using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Enums;

namespace TASLibrary.Models
{
    [Serializable]
    public class SeatModel : IEquatable<SeatModel>, ISerializable
    {
        public int No { get; set; }
        public PassengerModel Passenger { get; set; } = new PassengerModel();
        public SeatStatus Status { get; set; }

        public SeatModel()
        {

        }

        public SeatModel(int no, PassengerModel passenger, SeatStatus status)
        {
            No = no;
            Passenger = passenger;
            Status = status;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SeatModel);
        }
        public bool Equals(SeatModel other)
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
            return (No == other.No && Passenger == other.Passenger && Status == other.Status);
        }

        public override int GetHashCode()
        {
            int hashCode = 109669428;
            hashCode = hashCode * -1521134295 + No.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PassengerModel>.Default.GetHashCode(Passenger);
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(SeatModel lhs, SeatModel rhs)
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
        public static bool operator !=(SeatModel lhs, SeatModel rhs)
        {
            return !(lhs == rhs);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("No", No, typeof(int));
            info.AddValue("Passenger", Passenger, typeof(PassengerModel));
            info.AddValue("Status", Status, typeof(SeatStatus));

        }
        public SeatModel(SerializationInfo info, StreamingContext context)
        {
            No = (int)info.GetValue("No", typeof(int));
            Passenger = (PassengerModel)info.GetValue("Passenger", typeof(PassengerModel));
            Status = (SeatStatus)info.GetValue("Status", typeof(SeatStatus));
        }
    }
}
