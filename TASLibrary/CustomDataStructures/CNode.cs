using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.CustomDataStructures
{
    [Serializable]
    public class CNode<T> : IEquatable<CNode<T>>, ISerializable where T : class
    {
        public T Data;
        public CNode<T> Next;
        public CNode<T> Prev;
        public int Index { get; set; }
        public CNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;

        }
        public CNode()
        {
            Data = default(T);
            Next = null;
            Prev = null;
            Index = 0;
        }
        public void DeleteNode()
        {
            Next = null;
            Prev = null;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CNode<T>);
        }
        public bool Equals(CNode<T> other)
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
            return (Data == other.Data && Next == other.Next && Prev == other.Prev);
        }

        public override int GetHashCode()
        {
            int hashCode = 1272966573;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Data);
            return hashCode;
        }

        public static bool operator ==(CNode<T> lhs, CNode<T> rhs)
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
        public static bool operator !=(CNode<T> lhs, CNode<T> rhs)
        {
            return !(lhs == rhs);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Data", Data, typeof(T));
        }
        public CNode(SerializationInfo info, StreamingContext context)
        {
            Data = (T)info.GetValue("Data", typeof(T));
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
