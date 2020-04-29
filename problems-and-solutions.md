---
description: >-
  Some of the problems that faced us during developing the program and how we
  solved them.
---

# Problems & Solutions

## Proje Sorun ve Çözümleri 

CLinkedList class’ının fonksiyonlarını yazıp arayüzde bunları kullanmaya başladığımızda bazı problemlerle karşılaştık. Bunlar; 

1. WPF bazı elementlerinin ItemSource özelliğine atanan değişken tipinin IEnumerable interface’inin implemente olması gerektiği için class’ımıza GetEnumerator\(\) fonksiyonunu yazarak implement ettik. 
2.   Arayüz üzerinde listede bir değişiklik olduğunda arayüz üzerinde görünmüyordu. Bu sorunu gidermek için class’ımıza INotifyCollectionChanged interface’ini NotifyCollectionChangedEventHandler eventini kullanarak implemente ettik. 
3.  CNode classı generic olduğu için karşılaştırma işlemlerini IEquatable interface’i bütün model class’larına implemente ederek gerçekleştirdik.

