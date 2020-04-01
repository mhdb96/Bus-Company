using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.Versioning;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Collections.Specialized;

namespace TASLibrary.CustomDataStructures
{
    [Serializable]
    public class CLinkedList<T> : ICollection<T>, IEquatable<CLinkedList<T>>, ISerializable, IDeserializationCallback, INotifyCollectionChanged where T : class
    {
        private CNode<T> _head;
        private CNode<T> _tail;
        private int _count;
        private SerializationInfo siInfo;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T First {
            get
            {
                return _head.Data;
            }
        }
        public T Last
        {
            get
            {
                return _tail.Data;
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get
            {
                return InternalFind(index).Data;
            }
            set
            {
                index += 1;
                InternalFind(index).Data = value;
            }
        }
        public CLinkedList()
        {
            _count = 0;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            if (_count != 0)
            {
                T[] array = new T[_count];
                CopyTo(array, 0);                
                info.AddValue("values", array, typeof(T[]));
            }
        }
        public CLinkedList(SerializationInfo info, StreamingContext context)
        {
            siInfo = info;
        }
        private void AddNodeToEmptyList(CNode<T> newNode)
        {
            Debug.Assert(_head == null && _count == 0, "Can't use this function if the list is not empty!");
            newNode.Next = null;
            newNode.Prev = null;
            _head = newNode;
            _tail = _head;
        }
        public void AddLast(T data)
        {
            CNode<T> newNode = new CNode<T>(data);
            if (_head == null)
            {
                AddNodeToEmptyList(newNode);
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
                _tail = newNode;
            }
            _count++;
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,newNode.Data));
            }
        }
        public void AddFirst(T data)
        {
            CNode<T> newNode = new CNode<T>(data);
            if (_head == null)
            {
                AddNodeToEmptyList(newNode);
            }
            else
            {
                newNode.Next = _head;
                _head.Prev = newNode;
                _head = newNode;
            }
            _count++;
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newNode.Data));
            }
        }
        public void Clear()
        {
            CNode<T> currentNode = _head;
            while (currentNode != null)
            {
                CNode<T> tNode = currentNode;
                currentNode = currentNode.Next;
                tNode.DeleteNode();
            }
            _head = null;
            _tail = null;
            _count = 0;
        }
        public void RemoveFirst()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Can't remove the first element in an empty list!");
            }
            else
            {
                CNode<T> tNode = _head;
                if (_count == 1)
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    _head = _head.Next;
                    _head.Prev = null;
                }                                
                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, tNode.Data,0));
                }
                tNode.DeleteNode();
                _count--;
            }
        }
        public void RemoveLast()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Can't remove the first element in an empty list!");
            }
            else
            {
                CNode<T> tNode = _tail;
                if (_count == 1)
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    _tail = _tail.Prev;
                    _tail.Next = null;
                }                                
                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, tNode.Data, _count-1));
                }
                tNode.DeleteNode();
                _count--;
            }
        }
        
        public void RemoveAt(int index)
        {
            if (index == 0)
            {
                RemoveFirst();
            }
            else if(index == _count -1)
            {
                RemoveLast();
            }
            else
            {
                CNode<T> tempNode = InternalFind(index);
                tempNode.Prev.Next = tempNode.Next;
                tempNode.Next.Prev = tempNode.Prev;
                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, tempNode.Data, index));
                }
                tempNode.DeleteNode();
                _count--;
            }            
        }
        public bool Remove(T data)
        {
            CNode<T> tempNode = InternalFind(data);
            if(tempNode == null)
            {
                return false;
            }
            else
            {
                if(tempNode == _head)
                {
                    RemoveFirst();
                }
                else if(tempNode == _tail)
                {
                    RemoveLast();
                }
                else
                {
                    tempNode.Prev.Next = tempNode.Next;
                    tempNode.Next.Prev = tempNode.Prev;
                    if (CollectionChanged != null)
                    {
                        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, tempNode.Data, tempNode.Index));
                    }
                    tempNode.DeleteNode();
                    _count--;
                }
                return true;
            }
        }

        private CNode<T> InternalFind(int index)
        {
            if (index >= _count)
            {
                throw new ArgumentOutOfRangeException();
            }
            CNode<T> currentNode;
            if (index < _count / 2)
            {
                currentNode = _head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode;
            }
            else
            {
                currentNode = _tail;
                for (int i = 0; i < _count - 1 - index; i++)
                {
                    currentNode = currentNode.Prev;
                }
                return currentNode;
            }
        }
        private CNode<T> InternalFind(T data)
        {
            CNode<T> tempNode = _head;
            int index = 0;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (tempNode != null)
            {
                if (data != null)
                {
                    do
                    {
                        tempNode.Index = index;
                        if (c.Equals(tempNode.Data, data))
                        {
                            return tempNode;
                        }
                        tempNode = tempNode.Next;
                        index++;
                    } while (tempNode != null);
                }
            }
            return null;
        }
        public T Find(T data)
        {
            if(_head != null)
            {
                return InternalFind(data).Data;
            }
            return default(T);            
        }

        public T Find(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException();
            }
            //??? Contract.EndContractBlock();
            CNode<T> currentNode = _head; 
            while (currentNode != null)
            {
                if (match(currentNode.Data))
                {
                    return currentNode.Data;
                }
                currentNode = currentNode.Next;
            }
            return default(T);
        }

        public CLinkedList<T> FindAll(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException();
            }
            //??? Contract.EndContractBlock();
            CLinkedList<T> results = new CLinkedList<T>();
            CNode<T> currentNode = _head;
            while (currentNode != null)
            {
                if (match(currentNode.Data))
                {
                    results.AddLast(currentNode.Data);
                }
                currentNode = currentNode.Next;
            }

            return results;
        }

        public void AddBefore(int index)
        {

        }
        public void AddAfter(int index)
        {

        }


        private void CreateList(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                AddLast(array[i]);
            }
        }

        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
        public IEnumerator<T> GetEnumerator()
        {
            CNode<T> currentNode = _head;
            for (int i = 0; i < _count; i++)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
            //while (currentNode != null)
            //{
            //    yield return currentNode.Data;
            //    currentNode = currentNode.Next;
            //}
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CLinkedList<T>);
        }
        public bool Equals(CLinkedList<T> other)
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
            return (_head == other._head && _count == other._count);
        }

        public override int GetHashCode()
        {
            int hashCode = -33681659;
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_head);
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_tail);
            hashCode = hashCode * -1521134295 + _count.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(CLinkedList<T> lhs, CLinkedList<T> rhs)
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
        public static bool operator !=(CLinkedList<T> lhs, CLinkedList<T> rhs)
        {
            return !(lhs == rhs);
        }
        public void Add(T data)
        {
            AddLast(data);
        }

        public void OnDeserialization(object sender)
        {
            T[] array = (T[])siInfo.GetValue("values", typeof(T[]));
            CreateList(array);
        }

        public bool Contains(T data)
        {
            return Find(data) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CNode<T> currentNode = _head;
            if (currentNode != null)
            {
                do
                {
                    array[arrayIndex++] = currentNode.Data;
                    currentNode = currentNode.Next;
                } while (currentNode != null);
            }
        }
        public override string ToString()
        {            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($"{typeof(T).Name} List's Information:");
            sb.AppendLine("-----------------------");
            CNode<T> currentNode = _head;
            int i = 1;
            if (currentNode != null)
            {
                do
                {
                    sb.Append($"{i}th {currentNode.Data}");
                    currentNode = currentNode.Next;
                    i++;
                } while (currentNode != null);
            }            
            return sb.ToString();
        }
        public string ToString(string listName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{listName} List's Information:");
            sb.AppendLine("-----------------------");
            CNode<T> currentNode = _head;
            int i = 1;
            if (currentNode != null)
            {
                do
                {
                    sb.Append($"{i}th {currentNode.Data}");
                    currentNode = currentNode.Next;
                    i++;
                } while (currentNode != null);
            }
            return sb.ToString();
        }

    }
}
