using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Enums;

namespace TASLibrary.Models
{
    public class SeatModel : IEquatable<SeatModel>
    {
        public int No { get; set; }
        public PassengerModel Passenger { get; set; }
        public SeatStatus Status { get; set; }

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
    }
}
