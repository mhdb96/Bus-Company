using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Specialized;

namespace TASLibrary.CustomDataStructures
{
    /// <summary>
    /// Basic Linked list class with some interfaces implemented to work with WPF UI elements
    /// </summary>
    /// <typeparam name="T">List's node data type</typeparam>
    [Serializable]
    public class CLinkedList<T> : 
        ICollection<T>, 
        IEquatable<CLinkedList<T>>, 
        ISerializable, 
        IDeserializationCallback, 
        INotifyCollectionChanged where T : class
    {
        private CNode<T> _head;
        private CNode<T> _tail;
        private int _count;
        private SerializationInfo _siInfo;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// List first element's data
        /// </summary>
        public T First {
            get
            {
                return _head.Data;
            }
        }
        
        /// <summary>
        /// List last element's data
        /// </summary>
        public T Last
        {
            get
            {
                return _tail.Data;
            }
        }
        
        /// <summary>
        /// List elements' count
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Finds object data at a given index 
        /// </summary>
        /// <param name="index">requested element's index</param>
        /// <returns>The object at the sent index</returns>
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
        
        /// <summary>
        /// Serializes the list by converting it to Array and uses thier default serializer 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
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
        
        /// <summary>
        /// Saves SerializationInfo to the global variable siInfo
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public CLinkedList(SerializationInfo info, StreamingContext context)
        {
            _siInfo = info;
        }
        
        /// <summary>
        /// Adds the first node to the empty list 
        /// </summary>
        /// <param name="newNode">node to be added to the list</param>
        private void AddNodeToEmptyList(CNode<T> newNode)
        {
            Debug.Assert(_head == null && _count == 0, "Can't use this function if the list is not empty!");
            newNode.Next = null;
            newNode.Prev = null;
            _head = newNode;
            _tail = _head;
        }
        
        /// <summary>
        /// Adds new object to the end of the list
        /// </summary>
        /// <param name="data">Object to be added</param>
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
        
        /// <summary>
        /// Adds new object to the start of the list
        /// </summary>
        /// <param name="data">Object to be added</param>
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
        
        /// <summary>
        /// Deletes all the elements and resets the list 
        /// </summary>
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
        
        /// <summary>
        /// Removes the first element of the list
        /// </summary>
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
        /// <summary>
        /// Removes the last element of the list
        /// </summary>
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
        
        /// <summary>
        /// Removes element at a given index 
        /// </summary>
        /// <param name="index">Element's index</param>
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
        /// <summary>
        /// Removes a given element
        /// </summary>
        /// <param name="data">Element to be removed</param>
        /// <returns>true if element removed successfuly</returns>
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

        /// <summary>
        /// Finds node at a given index
        /// </summary>
        /// <param name="index">Node's index</param>
        /// <returns>the node at the sent index</returns>
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
        /// <summary>
        /// Finds a given element
        /// </summary>
        /// <param name="data">Element to be found</param>
        /// <returns>the node of the sent element</returns>
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
        /// <summary>
        /// Finds a given element
        /// </summary>
        /// <param name="data">Element to be found</param>
        /// <returns>the found element</returns>
        public T Find(T data)
        {
            if(_head != null)
            {
                return InternalFind(data).Data;
            }
            return default(T);            
        }

        /// <summary>
        /// Finds the first element matches the predicate
        /// </summary>
        /// <param name="match">Condition to be checked</param>
        /// <returns>the first element matching the predicate</returns>
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

        /// <summary>
        /// Finds all the elements matche the predicate
        /// </summary>
        /// <param name="match">Condition to be checked</param>
        /// <returns>all the elements matching the predicate</returns>
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

        //public void AddBefore(int index)
        //{

        //}
        //public void AddAfter(int index)
        //{

        //}


        /// <summary>
        /// Converts an array into List
        /// </summary>
        /// <param name="array">Array to be converted</param>
        private void CreateList(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                AddLast(array[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as CLinkedList<T>);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = -33681659;
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_head);
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_tail);
            hashCode = hashCode * -1521134295 + _count.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Checks for equality
        /// </summary>
        /// <param name="lhs">List on the left hand side</param>
        /// <param name="rhs">List on the right hand side</param>
        /// <returns>true if the lists have the same elements</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">List on the left hand side</param>
        /// <param name="rhs">List on the right hand side</param>
        /// <returns>true if the lists do not have the same elements</returns>
        public static bool operator !=(CLinkedList<T> lhs, CLinkedList<T> rhs)
        {
            return !(lhs == rhs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            AddLast(data);
        }

        /// <summary>
        /// Create the list on deserialization
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeserialization(object sender)
        {
            T[] array = (T[])_siInfo.GetValue("values", typeof(T[]));
            CreateList(array);
        }

        /// <summary>
        /// Checks if a given element exists in the list
        /// </summary>
        /// <param name="data">Element to be checked</param>
        /// <returns>true if the list has the element</returns>
        public bool Contains(T data)
        {
            return Find(data) != null;
        }

        /// <summary>
        /// Copies list's elements to a new array
        /// </summary>
        /// <param name="array">Array to be filled</param>
        /// <param name="arrayIndex">Start index</param>
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
        /// <summary>
        /// Converts all list elements' data to readable string
        /// </summary>
        /// <returns>formated string for list's all elements</returns>
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
        /// <summary>
        /// Converts all list elements' data to readable string
        /// </summary>
        /// <param name="listName">List's name</param>
        /// <returns>formated string for list's all elements</returns>
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
