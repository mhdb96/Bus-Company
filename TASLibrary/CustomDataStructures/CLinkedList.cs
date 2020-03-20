using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.CustomDataStructures
{
    public class CLinkedList <T> : IEnumerable<T>
    {
        public CNode<T> header = new CNode<T>();
        public int Count { get; set; }

        public CLinkedList()
        {
            Count = 0;
        }
        
        public void Add(T data)
        {
            if(header.Data == null)
            {
                header.Data = data;
                header.Next = null;
            }
            else
            {
                CNode<T> currentNode = header;
                while(currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                CNode<T> newNode = new CNode<T>(data);
                currentNode.Next = newNode;
                newNode.Next = null;
            }
            Count++;
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            CNode<T> currentNode = header;
            while (currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
