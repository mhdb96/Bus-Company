using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASLibrary.Enums;
using TASUI.Requesters;
using System.Xml; // bu daha sonra kaldıralacak.
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace TASUI.CreateForms
{
    /// <summary>
    /// Interaction logic for CreateTripWindow.xaml
    /// </summary>
    public partial class CreateTripWindow : Window
    {
        public ICreateTripRequester CallingWindow;

        CLinkedList<DestinationModel> Destinations;
        CLinkedList<BusModel> Buses;
        CLinkedList<DriverModel> Drivers;
        string filepath = @"C:\Users\mhdb9\Documents\GitHub\Bus-Company\trips.txt";

        public CreateTripWindow(/*ICreateTripRequester caller*/)
        {
            InitializeComponent();
            //CallingWindow = caller;

            LoadListsData();
        }

        private void LoadListsData()
        {
            Destinations = DestinationModel.GetSampleData();
            destinationsCombobox.ItemsSource = Destinations;
            Buses = BusModel.GetSampleData();
            busesCombobox.ItemsSource = Buses;
            Drivers = DriverModel.GetSampleData();
            driversCombobox.ItemsSource = Drivers;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CallingWindow.CreateTripFormClosed();
        }

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            TripModel model = new TripModel();
            model.No = int.Parse(tripCodeTextBox.Text);
            model.Destination = (DestinationModel)destinationsCombobox.SelectedItem;
            model.Bus = (BusModel)busesCombobox.SelectedItem;
            model.Driver = (DriverModel)driversCombobox.SelectedItem;

            DateTime d = (DateTime)tripDate.SelectedDate;
            DateTime t = (DateTime)tripTime.SelectedTime;
            model.Date = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

            IFormatter formatter = new BinaryFormatter();

            CreateTripWindow.SerializeItem(filepath, formatter, model); // Serialize an instance of the class.
            CreateTripWindow.DeserializeItem(filepath, formatter); // Deserialize the instance.
            Console.WriteLine("Done");
            Console.ReadLine();


            //CLinkedList<TripModel> list = new CLinkedList<TripModel>();
            //list.AddLast(model);

            //using (Stream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(CLinkedList<TripModel>));
            //    serializer.Serialize(fs, list);
            //}

            //using (TextWriter tw = new StreamWriter(filepath))
            //{
            //    foreach (var item in list)
            //    {
            //        tw.WriteLine(string.Format("Item: {0} - Cost: {1}", item));
            //    }
            //}
        }

        public static void SerializeItem(string fileName, IFormatter formatter, TripModel model)
        {
            FileStream s = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(s, model);
            s.Close();
        }

        public static void DeserializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            TripModel t = (TripModel)formatter.Deserialize(s);
            Console.WriteLine(t.No);
            Console.WriteLine(t.Destination.Name);
            Console.WriteLine(t.Date);
            Console.WriteLine(t.Bus.Plate);
            Console.WriteLine(t.Driver.Name);
            Console.WriteLine(t.SeatPrice);
        }
    }
}
