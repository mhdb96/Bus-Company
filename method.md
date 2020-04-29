---
description: These are the phases in which we developed the system.
---

# Method

## Model Oluşturma 

Projemize ilk olarak istenen özellikleri okuyarak başladık. Ardından sisteme lazım olan bileşenleri belirleyip model haline getirdik. Her modelin özelliklerini düşünerek alanlarını ve tiplerini belirledik.

## Arayüz ve Algoritma Taslağı Hazırlama

Projenin isterlerini okuduktan sonra nasıl bir arayüze sahip olması gerektiğine karar verdik. İlk önce admin olarak sisteme giriş yapan kişinin karşısına çıkacak ekranın taslağını çizdik ve içerisinde yapılan sefer işlemlerinin listesini oluşturduk. Daha sonra kullanıcı arayüzünün taslağını oluşturmaya başladık. Bu bölümde biletin satın alma algoritmasını çıkardık. Programın akış diyagramını oluşturduk.

## Yazılım Mimarisi Belirlenmesi 

Yaptığımız araştırmalar, hazırladığımız algoritmalar ve taslaklardan yola çıkarak geliştireceğimiz sistemin mimari tasarımını belirledik. Bu mimari tasarım iki kısımdan oluşmaktadır. Bunlardan birincisi class kütüphanesidir. TASLibrary \(Ticket Automation System Library\) kütüphanesi LinkedList yapısı, Model ve DataAccess classlarından oluşmaktadır. İkincisi ise TASUI \(Ticket Automation System User Interface\) WPF uygulamasından oluşur. Arayüz kodları burada bulunmaktadır.

## Linkedlist Araştırması 

Daha önceden linkedlist yapısını biliyorduk ve hız açısından daha verimli olacağını düşündüğümüz için projemizi single linked list değil double linked list şeklinde oluşturmaya karar verdik. Bu listeyi uygulamamızda herhangi bir veri tipiyle kullanabilmek için “Generic Class” olarak yazdık. `CLinkedList` class’ını `add()` fonksiyonu ile prototip aşamasında kullanılabilir hale getirdik.

## Prototip Tasarlama 

Arayüz işlemleri aşamasında hazırladığımız taslakları WPF framework’ünü kullanarak kodladık. `CLinkedList` class’ındaki `add()` fonksiyonunu kullanarak hazırladığımız arayüzü dinamik hale getirdik.

## Linkedlist Class’ını Yazma 

Yazacağımız `CLinkedList` class’ının her yazılımcı tarafından kullanılabilir olması için Microsoft’un .net’deki “LinkedList” ve “List” class’larının dokümantasyonuna bakarak kendi class’ımızı yazmaya başladık. Bu listenin nodu olarak `CNode` class’ını oluşturduk. Ardından listedeki node’ları manipüle eden fonksiyonları yazdık. Bu fonksiyonlardan bazılarına örnek verecek olursak bunlar; `AddLast()`, `RemoveLast()`, `Find()` vb.

## Proje Sorun ve Çözümleri 

`CLinkedList` class’ının fonksiyonlarını yazıp arayüzde bunları kullanmaya başladığımızda bazı problemlerle karşılaştık. Bunlar;

{% hint style="success" %}
WPF bazı elementlerinin ItemSource özelliğine atanan değişken tipinin `IEnumerable` interface’inin implemente olması gerektiği için class’ımıza `GetEnumerator()` fonksiyonunu yazarak implement ettik.
{% endhint %}

{% hint style="success" %}
Arayüz üzerinde listede bir değişiklik olduğunda arayüz üzerinde görünmüyordu. Bu sorunu gidermek için class’ımıza `INotifyCollectionChanged` interface’ini `NotifyCollectionChangedEventHandler` eventini kullanarak implemente ettik.
{% endhint %}

{% hint style="success" %}
CNode classı generic olduğu için karşılaştırma işlemlerini `IEquatable` interface’i bütün model class’larına implemente ederek gerçekleştirdik.
{% endhint %}

## Arayüzü Dinamik Hale Getirme 

Prototip aşamasında hazırladığımız arayüz tasarımını WPF ve MaterialDesignXAML framework’ünü kullanarak sefer ekleme, seferleri listeleme ve bilet satın alma sayfalarını kodladık. Daha sonra `CLinkedList` class’ındaki fonksiyonları kullanarak arayüzü dinamik hale getirdik. Hem formlar arasında veri transferi yapabilmek için hem de formları birbirine bağlı kalmaması için loose coupling design pattern’ini kullanarak formları bağladık. Bilet alma işleminde otobüsün koltuk sayısı kadar alan eklemek için `SeatUserControl` kullanarak dinamik bir şekilde arayüzde koltuk eklenebilir hale getirdik.

## Dosya Yazma ve Okuma 

Verilerimizin kaydedilmesi projemizin en önemli noktalarından biriydi. Bizde bu işin nasıl yapılması gerektiğini araştırmaya başladık. İlk başta verileri dosyaya yazma formatlarından \(Binary, XML, SOAP, CSV\) Binary formatını hızlı ve güvenli olduğu için kullanmaya karar verdik. Arayüzde yaptığımız değişiklikleri Binary formatında dosyaya yazmaya başladık. Daha sonra `CLinkedList` ve Modellere Serializable özelliği eklenmeden Binary Serializer’ın çalışmadığını fark ederek ekledik. C\# kendi kendine class’ları Serialize edebiliyor ancak biz belirli bir formatta Serialize etmesini istediğimiz için ISerializable interface’ini implemente ettik. Bu interface ile dosyaya belirlenen modeldeki alanları yazma işlemini ve dosyadan okuma işlemini gerçekleştirdik.

Projemizde veri depolama şeklinin esnek olması için `IDataConnection` adında bir interface oluşturduk. Bu interface’i implement ederek ister text dosyası isterse veri tabanı şeklinde bu interface’i arayüzde kullanan kodları değiştirmeden yeni bir depolama tipine geçiş yapılabilir hale getirdik. Ardından `TextFileConnector` class’ına `IDataConnection`’ı implement ederek gerekli fonksiyonları yazdık.

Proje isteklerinde sefer bilgilerinin dosyada okunabilir şekilde tutulması istendiği için bütün oluşturduğumuz data structure class’larımızda `ToString()` fonksiyonunu override ederek text dosyasında UTF-8 formatında yazdık.

## Sefer Numarası ve Toplam Sefer Sayısı İşlemleri 

Proje isteklerine göre seferlerin farklı dosyalarda tutulacağı için en son eklenen seferin numarasını takip etmek zaman kaybı oluyordu. Bu sorunu ortadan kaldırmak için dbinfo.info adında dosya oluşturarak en son eklenen seferin numarasını burda sakladık. Her sefer eklendiğinde aynı dosya üzerinde sefer numarasını arttırıyoruz.

Proje isteklerinden diğer biride toplam sefer sayısını göstermek olduğu için dbinfo.info dosyasında bu bilgiyi saklamaya başladık. Sefer eklendiğinde bu bilgiyi arttırıyoruz sefer silindiğinde ise azaltarak güncelliyoruz.

## Log Kaydı 

Veri tabanı işlemlerinde yapılan işlemlerin geçmişinin tutulması büyük önem arz ettiği için `Logger` class’ı oluşturduk. Yapılan değişiklikler her gün için ayrı bir log dosyasında saklanmaktadır. Log dosyasının içinde sefer ekleme, silme, güncelleme bilgilerinin yanı sıra bilet satın alma işlemleri tutulmaktadır. 

## Design Patterns 

Hem kendimizi design pattern konusunda geliştirmek hem de projenin belli stantardlar üzerinde geliştirilmesi için birkaç tane design pattern’i kodlarımızda kullandık. Bunlar;

### 1- SOLID: 

Yazılımın esnek, yeniden kullanılabilir, sürdürülebilir ve anlaşılır olmasını sağlayan ve kod tekrarını önleyen yazılım prensibidir. Single-responsibility principle `DataAccess` class’ında kullandık.

### 2- Builder: 

Çok alanlı class’ların objelerini constructor kullanmadan oluşturabilmek için kullanılan bir prensiptir. `FileDbInfo` class’ı için kullandık.

### 3- Loose Coupling: 

Formları bağlarken tight coupling değil de loose coupling ile bağlama esnekliğini sağladık.

### 4- Fluent API: 

Akıcı bir arayüz, tasarımı büyük ölçüde yöntem zincirlemeye dayanan bir nesne yönelimli API'dir. Amacı, alana özgü bir dil oluşturarak kod okunabilirliğini artırmaktır. Bu yöntemi `FileDbInfoBuilder` class’ında kullandık.

### 5- Model: 

Veri depolama alanlarını nesneye dönüştürerek manipüle etmemizi sağlayan bir prensiptir.

