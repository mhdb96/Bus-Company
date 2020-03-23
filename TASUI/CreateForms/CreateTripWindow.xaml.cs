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
using TASLibrary;
using TASLibrary.Enums;
using TASUI.Requesters;
using System.Xml; // bu daha sonra kaldıralacak.
using System.Xml.Serialization;
using System.IO;

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

        TripModel Trip = new TripModel();
        string filepath = "C:\\Users\\Talha\\source\\repos\\Bus-Company\\trips.txt";

        public CreateTripWindow(ICreateTripRequester caller)
        {
            InitializeComponent();
            CallingWindow = caller;

            LoadListsData();
        }

        private void LoadListsData()
        {
            Destinations = GlobalConfig.Connection.GetDestination_All();
            destinationsCombobox.ItemsSource = Destinations;
            Buses = GlobalConfig.Connection.GetBus_All();
            busesCombobox.ItemsSource = Buses;
            Drivers = GlobalConfig.Connection.GetDriver_All();
            driversCombobox.ItemsSource = Drivers;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CallingWindow.CreateTripFormClosed();
        }

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            int tripCode = int.Parse(tripCodeTextBox.Text);
            string destination = destinationsCombobox.SelectedItem.ToString();
            string bus = busesCombobox.SelectedItem.ToString();
            string driver = driversCombobox.SelectedItem.ToString();
            string date = tripDate.SelectedDate.ToString();
            string time = tripTime.SelectedTime.ToString();

            CLinkedList<DestinationModel> destinationData = new CLinkedList<DestinationModel>();
            destinationData.AddLast(new DestinationModel(destination));

            CLinkedList<BusModel> busData = new CLinkedList<BusModel>();
            busData.AddLast(new BusModel(bus, 20));

            CLinkedList<DriverModel> driverData = new CLinkedList<DriverModel>();
            driverData.AddLast(new DriverModel(driver));

            Trip.z = new CLinkedList<TripModel> {
                //new TripModel { No = tripCode, Destination = destinationData },
            };
            Trip.XMLKaydet();
        }
    }
}
