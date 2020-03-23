using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Enums;

namespace TASLibrary.Models
{
    public class PassengerModel : IEquatable<PassengerModel>
    {
        public string Name { get; set; }
        public SexType Sex { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PassengerModel);
        }
        public bool Equals(PassengerModel other)
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
            return (Name == other.Name && Sex == other.Sex);
        }

        public override int GetHashCode()
        {
            int hashCode = 1423121277;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Sex.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PassengerModel lhs, PassengerModel rhs)
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
        public static bool operator !=(PassengerModel lhs, PassengerModel rhs)
        {
            return !(lhs == rhs);
        }
    }
}
