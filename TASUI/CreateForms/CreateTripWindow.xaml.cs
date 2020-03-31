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
using TASUI.Requesters;
using TASLibrary;

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
        TripModel editTripData;
        bool isUpdate = false;
        int LastId;

        public CreateTripWindow(ICreateTripRequester caller, int id)
        {
            InitializeComponent();
            CallingWindow = caller;
            
            LoadListsData();

            tripCodeTextBox.Text = id.ToString();
        }
        public CreateTripWindow(ICreateTripRequester caller, TripModel model)
        {
            InitializeComponent();
            CallingWindow = caller;
            editTripData = model;
            LoadListsData();

            // fill all the fields for update
            this.Title = $"Update {editTripData.No.ToString()} Trip";

            tripCodeTextBox.Text = editTripData.No.ToString();
            destinationsCombobox.SelectedItem = editTripData.Destination;
            busesCombobox.SelectedItem = editTripData.Bus;
            seatPriceTextBox.Text = editTripData.SeatPrice.ToString();
            driversCombobox.SelectedItem = editTripData.Driver;
            tripDate.SelectedDate = editTripData.Date;
            tripTime.SelectedTime = editTripData.Date;

            AddNewTripButtonTextBlock.Text = "Update Trip";

            isUpdate = true;
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
            TripModel model = new TripModel();
            model.No = int.Parse(tripCodeTextBox.Text);
            model.Destination = (DestinationModel)destinationsCombobox.SelectedItem;
            model.Bus = (BusModel)busesCombobox.SelectedItem;
            model.SeatPrice = int.Parse(seatPriceTextBox.Text);
            model.Driver = (DriverModel)driversCombobox.SelectedItem;

            DateTime d = (DateTime)tripDate.SelectedDate;
            DateTime t = (DateTime)tripTime.SelectedTime;
            model.Date = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

            CallingWindow.TripCreated(model); 

            this.Close();
        }

        public void editTrip(TripModel model)
        {
            MessageBox.Show(model.No.ToString());
        }
    }
}
