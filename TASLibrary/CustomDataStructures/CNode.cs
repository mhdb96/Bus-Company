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

        public CNode(T data)
        {
            Data = data;
            Next = null;
        }
        public CNode()
        {
            Data = default(T);
            Next = null;
        }
    }
}
