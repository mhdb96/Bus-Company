---
description: >-
  Basic Linked list class with some interfaces implemented to work with WPF UI
  elements
---

# CLinkedList Class

CLinkedList class, is a custom C\# generic class implementing basic Doubly linked list operations with some extra features for ease of use and better integration with other .Net classes especially for WPF UI elements.  
We chose the class to be generic so the list elements can be of any type chosen by the end user

## CLinkedList Class

Namespace: TASLibrary.CustomDataStructures  
Assembly: TASLibrary.dll

Represents a doubly linked list.

{% code title="CLinkedList.cs" %}
```csharp
public class CLinkedList<T> : 
ICollection<T>, 
IEquatable<CLinkedList<T>>, 
ISerializable, 
IDeserializationCallback, 
INotifyCollectionChanged 
where T : class {}
```
{% endcode %}

### **Type Parameters**

`T` ****Specifies the element type of the linked list.

### Implements

[ICollection&lt;T&gt;](clinkedlist-class.md#icollection-less-than-t-greater-than), [IEquatable&lt;T&gt;](clinkedlist-class.md#iequatable-less-than-t-greater-than), [ISerializable](clinkedlist-class.md#iserializable), [IDeserializationCallback](clinkedlist-class.md#ideserializationcallback), [INotifyCollectionChanged](clinkedlist-class.md#inotifycollectionchanged)

## Remarks

CLinkedList is a general-purpose linked list. It supports enumerators and implements the ICollection interface, consistent with other collection classes in the .NET Framework. 

CLinkedList provides separate nodes of type CNode, so insertion and removal are O\(1\) operations. 

The list maintains an internal count, getting the Count property is an O\(1\) operation. 

Each node in a CLinkedList object is of the type CNode. Because the CLinkedList is doubly linked, each node points forward to the Next node and backward to the Previous node. 

If the CLinkedList is empty, the First and Last properties contain null. 

The CLinkedList class does not support chaining, splitting, cycles, or other features that can leave the list in an inconsistent state.

## Class Members

### Private Members:

* `_head`: The first node in the list of type `CNode<T>`.

```csharp
private CNode<T> _head;
```

* `_tail`: The last node in the list of type `CNode<T>`.

```csharp
private CNode<T> _tail;
```

* `_count`: Nodes count in the list of type `int`.

```csharp
private int _count;
```

* `_siInfo`; Serialization data used while deserialization of the Type `SerializationInfo`.

```csharp
private SerializationInfo _siInfo;
```

### Public Members

* `First`: Gets the first element in the list of type `T`.

```csharp
public T First { get { return _head.Data; } }
```

* `Last`: Gets the last element in the list of type `T`.

```csharp
public T Last { get { return _tail.Data; } }
```

* `Count`: Gets the number of nodes actually contained in the list.

```csharp
public int Count { get { return _count; } }
```

* `IsReadOnly`; Determines if the class is read only or not of type `bool`.

```csharp
public bool IsReadOnly { get { return false; } }
```

## Private Methods

### `AddNodeToEmptyList(CNode<T>)`

Adds the specified node to an empty list. 

```csharp
private void AddNodeToEmptyList(CNode<T> newNode)
{
    Debug.Assert(
        _head == null && _count == 0, 
        "Can't use this function if the list is not empty!");
    newNode.Next = null;
    newNode.Prev = null;
    _head = newNode;
    _tail = _head;
}
```

### `InternalFind(int)`

Finds the node at the specified index.

```csharp
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
```

### `InternalFind(T)`

Finds the node that contains the same specified data. 

```csharp
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
```

### `CreateList(T[])`

Initializes the list after deserialization from the specified array.   

```csharp
private void CreateList(T[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        AddLast(array[i]);
    }
}
```

## Public Methods

### `this[int]`

Gets or sets the element at the specified index.

```csharp
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
```

### `AddLast(T)`

Adds a new node containing the specified value at the end of the list.

```csharp
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
        CollectionChanged(this, 
        new NotifyCollectionChangedEventArgs(
        NotifyCollectionChangedAction.Add,
        newNode.Data));
    }
}
```

### `AddFirst(T)`

Adds a new node containing the specified value at the start of the list.

```csharp
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
        CollectionChanged(this, 
        new NotifyCollectionChangedEventArgs(
        NotifyCollectionChangedAction.Add, 
        newNode.Data));
    }
}
```

### `RemoveLast()`

 Removes the node at the end of the list.

```csharp
public void RemoveLast()
{
    if (_head == null)
    {
        throw new InvalidOperationException(
        "Can't remove the first element in an empty list!");
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
            CollectionChanged(this, 
            new NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction.Remove, 
            tNode.Data, 
            _count-1));
        }
        tNode.DeleteNode();
        _count--;
    }
}
```

### `RemoveFirst()`

 Removes the node at the start of the list.

```csharp
public void RemoveFirst()
{
    if (_head == null)
    {
        throw new InvalidOperationException(
        "Can't remove the first element in an empty list!");
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
            CollectionChanged(this, 
            new NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction.Remove, 
            tNode.Data,
            0));
        }
        tNode.DeleteNode();
        _count--;
    }
}
```

### `Remove(T)`

Removes the first occurrence of the specified value from the list.

```csharp
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
                CollectionChanged(this, 
                new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, 
                tempNode.Data, 
                tempNode.Index));
            }
            tempNode.DeleteNode();
            _count--;
        }
        return true;
    }
}
```

### `RemoveAt(int)`

 Removes the node at the specified index from the list.

```csharp
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
            CollectionChanged(this, 
            new NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction.Remove, 
            tempNode.Data, 
            index));
        }
        tempNode.DeleteNode();
        _count--;
    }            
}
```

### `Clear()`

Removes all nodes from the list.

```csharp
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
```

### `Find(T)`

Finds the first node that contains the specified value.

```csharp
public T Find(T data)
{
    if(_head != null)
    {
        return InternalFind(data).Data;
    }
    return default(T);            
}
```

### `Find(Predicate<T>)`

Finds the first node that matches the specified predicate.

```csharp
public T Find(Predicate<T> match)
{
    if (match == null)
    {
        throw new ArgumentNullException();
    }
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
```

### `FindAll(Predicate<T>)`

Finds all the nodes that match the specified predicate.

```csharp
public CLinkedList<T> FindAll(Predicate<T> match)
{
    if (match == null)
    {
        throw new ArgumentNullException();
    }
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
```

### `ToString()`

Returns a string that represents the current object. \(**Override**\)

```csharp
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
```

### `ToString(string)`

Returns a string that represents the current object with the list name inserted to it.

```csharp
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
```

## [ICollection&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=netcore-3.1)

Defines methods to manipulate generic collections.

### `GetEnumerator()`

Returns an enumerator that iterates through the linked list as a collection.

```csharp
public IEnumerator<T> GetEnumerator()
{
    CNode<T> currentNode = _head;
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
```

### `Add()`

Adds an item at the end of the list.

```csharp
public void Add(T data)
{
    AddLast(data);
}
```

### `CopyTo(T[], int)`

Copies the entire list to a compatible one-dimensional [Array](https://docs.microsoft.com/en-us/dotnet/api/system.array?view=netcore-3.1), starting at the specified index of the target array.

```csharp
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
```

### `Contains(T)`

Checks if a given element exists in the list.

```csharp
public bool Contains(T data)
{
    return Find(data) != null;
}
```

## [`IEquatable<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=netcore-3.1)

Defines a generalized method that a value type or class implements to create a type-specific method for determining equality of instances.

### `Equals(CLinkedList<T>)`

Determines whether the specified object is equal to the current object.

```csharp
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
```

### `Equals(CLinkedList<T>)`

Determines whether the specified object is equal to the current object. \(**Override**\)

```csharp
public override bool Equals(object obj)
{
    return this.Equals(obj as CLinkedList<T>);
}
```

### `GetHashCode`

Serves as the default hash function.

```csharp
public override int GetHashCode()
        {
            int hashCode = -33681659;
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_head);
            hashCode = hashCode * -1521134295 + EqualityComparer<CNode<T>>.Default.GetHashCode(_tail);
            hashCode = hashCode * -1521134295 + _count.GetHashCode();
            return hashCode;
        }
```

### `==(CLinkedList<T>, CLinkedList<T>)`

Determines whether the first object is equal to the other object.

```csharp
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
```

### `!=(CLinkedList<T>, CLinkedList<T>)`

Determines whether the first object is not equal to the other object.

```csharp
public static bool operator !=(CLinkedList<T> lhs, CLinkedList<T> rhs)
{
    return !(lhs == rhs);
}
```

## [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable?view=netcore-3.1)

Allows an object to control its own serialization and deserialization.

### `GetObjectData(SerializationInfo, StreamingContext)`

 Implements the [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable?view=netcore-3.1) interface and returns the data needed to serialize the list instance.

```csharp
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
```

### `CLinkedList<>(SerializationInfo, StreamingContext)`

 Initializes a new instance of the CLinkedList class that is serializable with the specified [SerializationInfo](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.serializationinfo?view=netcore-3.1) and [StreamingContext](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.streamingcontext?view=netcore-3.1).

```csharp
public CLinkedList(SerializationInfo info, StreamingContext context)
{
    siInfo = info;
}
```

## [IDeserializationCallback](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.ideserializationcallback?view=netcore-3.1)

Indicates that a class is to be notified when deserialization of the entire object graph has been completed.

### `OnDeserialization(object sender)`

Implements the [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable?view=netcore-3.1) interface and raises the deserialization event when the deserialization is complete.

```csharp
public void OnDeserialization(object sender)
{
    T[] array = (T[])siInfo.GetValue("values", typeof(T[]));
    CreateList(array);
}
```

## [INotifyCollectionChanged](https://docs.microsoft.com/en-us/dotnet/api/system.collections.specialized.inotifycollectionchanged?view=netcore-3.1)

Notifies listeners of dynamic changes, such as when an item is added and removed or the whole list is cleared.

### `CollectionChanged`

Occurs when the collection changes.

```csharp
public event NotifyCollectionChangedEventHandler CollectionChanged;
```

