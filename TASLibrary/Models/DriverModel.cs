﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.Models
{
    [Serializable]
    public class DriverModel : IEquatable<DriverModel>, ISerializable
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
            Data.AddLast(new DriverModel("Remzi Aslan"));
            Data.AddLast(new DriverModel("Can Hekimoglu"));
            Data.AddLast(new DriverModel("Ufuk Kantar"));
            Data.AddLast(new DriverModel("Furkan Korkmaz"));            
            return Data;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DriverModel);
        }
        public bool Equals(DriverModel other)
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

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(DriverModel lhs, DriverModel rhs)
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
        public static bool operator !=(DriverModel lhs, DriverModel rhs)
        {
            return !(lhs == rhs);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name, typeof(string));            
        }
        public DriverModel(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));            
        }
        public override string ToString()
        {                               
            return $"Driver's Information => Name: {Name}";
        }
    }
}
