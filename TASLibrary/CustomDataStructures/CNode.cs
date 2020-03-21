using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.CustomDataStructures
{
    public class CNode <T>
    {
        public T Data;
        public CNode<T> Next;
        public CNode<T> Prev;
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
        }
        public void DeleteNode()
        {
            Next = null;
            Prev = null;
        }
    }
}
