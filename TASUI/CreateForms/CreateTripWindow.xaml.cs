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
using TASLibrary.DataAccess;
using TASLibrary.Enums;
using System.Text.RegularExpressions;

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

        public CreateTripWindow(ICreateTripRequester caller, int id, DateTime selectedDate)
        {
            InitializeComponent();
            CallingWindow = caller;
            
            LoadListsData();

            tripDate.SelectedDate = selectedDate;

            tripCodeTextBox.Text = id.ToString();
        }
        public CreateTripWindow(ICreateTripRequester caller, TripModel model)
        {
            InitializeComponent();

            CallingWindow = caller;

            editTripData = model;

            LoadListsData();

            isUpdate = true;

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

            // if there is a sold seat you can't change the bus
            foreach (SeatModel seat in model.Seats)
            {
                if (seat.Status == SeatStatus.Sold || seat.Status == SeatStatus.Reserved)
                {
                    busesCombobox.IsEnabled = false;

                    break;
                }
            }
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

            bool isBusChanged = false;

            if (isUpdate)
            {
                model = editTripData;
                
                if (model.Bus != (BusModel)busesCombobox.SelectedItem)
                {
                    isBusChanged = true;
                }
            }            
            
            model.No = int.Parse(tripCodeTextBox.Text);
            model.Destination = (DestinationModel)destinationsCombobox.SelectedItem;
            model.Bus = (BusModel)busesCombobox.SelectedItem;
            model.SeatPrice = int.Parse(seatPriceTextBox.Text);
            model.Driver = (DriverModel)driversCombobox.SelectedItem;
            DateTime d = (DateTime)tripDate.SelectedDate;
            DateTime t = (DateTime)tripTime.SelectedTime;
            model.Date = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

            if(!isUpdate)
            {
                model.CreateSeats();

                CallingWindow.TripCreated(model);
            }
            else
            {
                if (isBusChanged)
                {
                    model.CreateSeats();
                }
                CallingWindow.TripUpdated(model);
            }                                
            this.Close();
        }

        private void seatPriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
