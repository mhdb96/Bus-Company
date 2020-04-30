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
        CLinkedList<TripModel> Trips;     
        DateTime selectedDate;
        bool isChange = false;        
        int TripCount = GlobalConfig.Connection.GetDbInfo(DbInfo.TripCount);
        int TripId = GlobalConfig.Connection.GetDbInfo(DbInfo.TripId);
        List<string> logs = new List<string>();
        bool IsChange
        {
            get => isChange;
            set
            {
                isChange = value;
                saveChangesButton.IsEnabled = value;
            }
        }
        public AdminPanelWindow()
        {
            InitializeComponent();                        
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
            int lastCreatedTripId = TripId + 1;
            CreateTripWindow createTrip = new CreateTripWindow(this, lastCreatedTripId, selectedDate);
            this.Hide();
            createTrip.ShowDialog();
        }        

        public void CreateTripFormClosed()
        {
            this.Show();
        }

        public void TripCreated(TripModel model)
        {      
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);
            // trip created log
            logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip {model.No} has been created.");
            TripId = model.No;
            // Trip id log
            logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip id updated to {TripId}.");
            TripCount++;
            // trip count log
            logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip count updated to {TripCount}.");
            IsChange = true;
        }
        public void TripUpdated(TripModel model)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);
            logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip {model.No} has been updated.");
            IsChange = true;
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
            bool isSold = false;
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
                // remove log
                logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip {model.No} has been deleted.");
                TripCount--;
                // trip count log
                logs.Add($"{DateTime.Now.ToLongTimeString()} - Trip count updated to {TripCount}.");
                IsChange = true;
            }
        }

        private void SaveList()
        { 
            if (IsChange)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save your changes?", "Warning", MessageBoxButton.YesNo); // TODO daha sonra material dialog eklenecek.
                if (result == MessageBoxResult.Yes)
                {
                    SaveOperations();
                }
                IsChange = false;
            }
        }

        private void SaveOperations()
        {
            // Trips insert to file
            GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);
            // Trip id update in file
            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripId, TripId);
            // Trip count update in file
            GlobalConfig.Connection.UpdateDbInfo(DbInfo.TripCount, TripCount);
            // All the logs write to log file
            GlobalConfig.Connection.Logger.WriteLogsToFile(logs);
            logs.Clear();
            tripCountTextBlock.Text = "All Trips Count: " + TripCount.ToString();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveOperations();
            MessageBox.Show("Changes are successfully updated.");
            IsChange = false;            
        }

        private void tripDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveList();
            Trips = GlobalConfig.Connection.GetTrip_All((DateTime)tripDate.SelectedDate);
            selectedDate = (DateTime)tripDate.SelectedDate;
            if (tripDate.SelectedDate < DateTime.Now.Date)
            {
                AddNewTripButton.IsEnabled = false;
            }
            else
            {
                AddNewTripButton.IsEnabled = true;
            }

            WireUpLists();
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

        public void SeatCreated(TripModel model, List<string> seatLogs)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);
            logs = logs.Concat(seatLogs).ToList();
            IsChange = true;
        }

        private void openDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(GlobalConfig.info.ProjectDirectory);
        }
    }
}
