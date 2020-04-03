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
    public partial class AdminPanelWindow : Window, ICreateTripRequester, ICreateSeatRequester
    {
        public IAdminPanelRequester CallingWindow;
        CLinkedList<TripModel> Trips;
        List<TripModel> TripList = new List<TripModel>();
        DateTime selectedDate;
        bool isChange = false;
        int TripCount = GlobalConfig.Connection.GetDbInfo(DbInfo.TripCount);
        int TripId = GlobalConfig.Connection.GetDbInfo(DbInfo.TripId);

        bool IsChange
        {
            get { return isChange; }
            set
            {
                isChange = value;
                saveChangesButton.IsEnabled = value;
            }
        }

        bool isSold = false;

        public AdminPanelWindow(/*IAdminPanelRequester caller*/)
        {
            InitializeComponent();
            //CallingWindow = caller;
            //Trips = TripModel.GetSampleData();
            selectedDate = DateTime.Now;
            tripDate.SelectedDate = selectedDate;
            Trips = GlobalConfig.Connection.GetTrip_All(DateTime.Now);

            IsChange = false;
            tripsDataGrid.ItemsSource = Trips;

            tripCountTextBlock.Text = "All Trips Count: " + TripCount.ToString();
        }

        private void WireUpLists()
        {
            tripsDataGrid.ItemsSource = null;
            tripsDataGrid.Items.Clear();
            tripsDataGrid.ItemsSource = Trips;
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
            TripCount++;

            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);

            TripId = model.No;

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

            foreach (var seat in model.Seats)
            {
                if (seat.Status == SeatStatus.Sold)
                {
                    isSold = true;
                    break;
                }
            }

            if (isSold)
            {
                MessageBox.Show("You can't delete this trip because there is a seat already sold ");
                isSold = false;
            }
            else
            {
                Trips.Remove(model);

                TripCount--;

                IsChange = true;
            }
        }

        private void SaveList()
        { 
            if (IsChange)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save your changes?", "warning", MessageBoxButton.YesNo); // TODO daha sonra material dialog eklenecek.

                if (result == MessageBoxResult.Yes)
                {
                    GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);

                    GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripId, TripId);

                    GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripCount, TripCount);

                    tripCountTextBlock.Text = "All Trips Count: " + TripCount.ToString();
                }

                IsChange = false;
            }
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);

            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripId, TripId);

            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripCount, TripCount);

            tripCountTextBlock.Text = "All Trips Count: " + TripCount.ToString();

            MessageBox.Show("Changes are successfully updated.");

            IsChange = false;            
        }

        private void tripDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveList();
            Trips = GlobalConfig.Connection.GetTrip_All((DateTime)tripDate.SelectedDate);
            selectedDate = (DateTime)tripDate.SelectedDate;
            WireUpLists();

            if (tripDate.SelectedDate < DateTime.Now.Date)
            {
                AddNewTripButton.IsEnabled = false;
            }
            else
            {
                AddNewTripButton.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveList();
        }

        private void manageSeatsButton_Click(object sender, RoutedEventArgs e)
        {
            TripModel model = (TripModel)tripsDataGrid.SelectedItem;

            CreateSeatWindow createSeat = new CreateSeatWindow(this, model);
            this.Hide();
            createSeat.ShowDialog();
        }

        public void CreateSeatFormClosed()
        {
            this.Show();
        }

        public void SeatCreated(TripModel model)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);

            WireUpLists();

            IsChange = true;
        }

        public void TripUpdated(TripModel model)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);

            IsChange = true;
            WireUpLists();
        }
    }
}
