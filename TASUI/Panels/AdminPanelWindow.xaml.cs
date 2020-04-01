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
using TASUI.Requesters;
using TASUI.CreateForms;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASLibrary.Enums;
using TASLibrary;

namespace TASUI.Panels
{
    /// <summary>
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window, ICreateTripRequester
    {
        public IAdminPanelRequester CallingWindow;
        CLinkedList<TripModel> Trips;
        List<TripModel> TripList = new List<TripModel>();
        DateTime selectedDate;
        bool isChange = false;
        bool IsChange
        {
            get { return isChange; }
            set
            {
                isChange = value;
                saveChangesButton.IsEnabled = value;
            }
        }


        public AdminPanelWindow(/*IAdminPanelRequester caller*/)
        {
            InitializeComponent();
            //CallingWindow = caller;
            //Trips = TripModel.GetSampleData();
            selectedDate = DateTime.Now;
            tripDate.SelectedDate = selectedDate;
            Trips = GlobalConfig.Connection.GetTrip_All(DateTime.Now);
            //test();
            IsChange = false;
            tripsDataGrid.ItemsSource = Trips;
        }

        private void WireUpLists()
        {
            tripsDataGrid.ItemsSource = null;
            tripsDataGrid.Items.Clear();
            tripsDataGrid.ItemsSource = Trips;
        }

        private void test()
        {
            //TripModel model = new TripModel();
            //model.No = 22;
            //model.Destination.Name = "Kocaeli";
            //model.Bus.Capacity = 10;
            //model.Bus.Plate = "ASD1234";
            //model.Driver.Name = "Ahmet";
            //model.Date = DateTime.Now;
            //model.Seats.AddLast(new SeatModel(1, new PassengerModel("muhammed", SexType.Male), SeatStatus.Sold));
            //TripModel test = new TripModel();
            //test.No = 22;
            //test.Destination.Name = "Locaeli";
            //test.Bus.Capacity = 17;
            //test.Bus.Plate = "ASD1234";
            //test.Driver.Name = "Ahmet";
            //test.Date = DateTime.Now;
            //test.Seats.AddLast(new SeatModel(1, new PassengerModel("Ahmad", SexType.Male), SeatStatus.Sold));
            //TripList.Add(model);
            //TripList.Add(test);
        }

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            int lastCreatedTripId = GlobalConfig.Connection.GetDbInfo(DbInfo.TripId) + 1;

            CreateTripWindow createTrip = new CreateTripWindow(this, lastCreatedTripId, selectedDate);
            this.Hide();
            createTrip.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //CallingWindow.AdminPanelClosed();
        }

        public void CreateTripFormClosed()
        {
            this.Show();
        }

        public void TripCreated(TripModel model)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);
            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripId, model.No);
            IsChange = true;
            WireUpLists();
        }

        private void editTripBtn_Click(object sender, RoutedEventArgs e)
        {
            TripModel model = (TripModel)tripsDataGrid.SelectedItem;

            CreateTripWindow createTrip = new CreateTripWindow(this, model);
            this.Hide();
            createTrip.ShowDialog();
        }

        private void deleteTripBtn_Click(object sender, RoutedEventArgs e)
        {
            TripModel model = (TripModel)tripsDataGrid.SelectedItem;
            Trips.Remove(model);
            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripCount, 0);
            IsChange = true;
        }

        private void SaveList()
        { 
            if (IsChange)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save your changes?", "warning", MessageBoxButton.YesNo); // TODO daha sonra material dialog eklenecek.

                if (result == MessageBoxResult.Yes)
                {
                    GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);
                    IsChange = false;
                }
            }
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);
            MessageBox.Show("Değişiklikler başarılı bir şekilde kayıt edildi.");
            IsChange = false;
        }

        private void tripDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveList();
            Trips = GlobalConfig.Connection.GetTrip_All((DateTime)tripDate.SelectedDate);
            selectedDate = (DateTime)tripDate.SelectedDate;
            WireUpLists();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveList();
        }
    }
}
