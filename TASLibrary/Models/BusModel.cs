using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    [Serializable]
    public class BusModel : IEquatable<BusModel>, ISerializable
    {
        public string Plate { get; set; }
        public int Capacity { get; set; }
        public string fullBusInfo { get { return Plate + " - " + Capacity.ToString() + " Seats"; } set { } }

        public BusModel ()
        {

        }

        public BusModel(string plate, int capacity)
        {
            Plate = plate;
            Capacity = capacity;
        }

        public static CLinkedList<BusModel> GetSampleData()
        {
            CLinkedList<BusModel> Data = new CLinkedList<BusModel>();
            Data.AddLast(new BusModel("34 KL 2598", 60));
            Data.AddLast(new BusModel("58 SV 1453", 40));
            Data.AddLast(new BusModel("41 KS 9654", 20));
            Data.AddLast(new BusModel("05 SV 2478", 12));
            Data.AddLast(new BusModel("06 LTK 253", 40));
            return Data;
        }
        
        public override bool Equals(object obj)
        {
            return this.Equals(obj as BusModel);
        }

        public bool Equals(BusModel other)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }
            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Plate == other.Plate) && (Capacity == other.Capacity);
        }
        public static bool operator ==(BusModel lhs, BusModel rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }
                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }
        public static bool operator !=(BusModel lhs, BusModel rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            int hashCode = -724381756;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Plate);
            hashCode = hashCode * -1521134295 + Capacity.GetHashCode();
            return hashCode;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Plate", Plate, typeof(string));
            info.AddValue("Capacity", Capacity, typeof(int));
        }
        public BusModel (SerializationInfo info, StreamingContext context)
        {            
            Plate = (string)info.GetValue("Plate", typeof(string));
            Capacity = (int)info.GetValue("Capacity", typeof(int));
        }
        public override string ToString()
        {            
            return $"Bus's Information => Plate: {Plate}, Capacity: {Capacity}";
        }
    }
}
