using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    public class DestinationModel : IEquatable<DestinationModel>
    {
        public string Name { get; set; }

        public DestinationModel()
        {

        }

        public DestinationModel(string name)
        {
            Name = name;
        }

        public static CLinkedList<DestinationModel> GetSampleData()
        {
            CLinkedList<DestinationModel> Data = new CLinkedList<DestinationModel>();
            Data.AddLast(new DestinationModel("Kocaeli - Ankara"));
            Data.AddLast(new DestinationModel("Kocaeli - Izmir"));
            Data.AddLast(new DestinationModel("Kocaeli - Istanbul"));                        
            return Data;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as DestinationModel);
        }


        public bool Equals(DestinationModel other)
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
            return (Name == other.Name);
        }

        public static bool operator ==(DestinationModel lhs, DestinationModel rhs)
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
        public static bool operator !=(DestinationModel lhs, DestinationModel rhs)
        {
            return !(lhs == rhs);
        }
    }
}
