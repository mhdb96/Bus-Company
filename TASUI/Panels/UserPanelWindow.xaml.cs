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
using TASLibrary;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;
using TASUI.CreateForms;
using TASUI.Requesters;

namespace TASUI.Panels
{
    /// <summary>
    /// Interaction logic for UserPanelWindow.xaml
    /// </summary>
    public partial class UserPanelWindow : Window, ICreateSeatRequester
    {
        //IUserPanelRequester CallingWindow;

        CLinkedList<TripModel> Trips;
        DateTime selectedDate;

        public UserPanelWindow(/*IUserPanelRequester caller*/)
        {
            InitializeComponent();
            //CallingWindow = caller;

            selectedDate = DateTime.Now;
            tripDate.SelectedDate = selectedDate; // change selected date to tripDate

            Trips = GlobalConfig.Connection.GetTrip_All(selectedDate);
            tripsDataGrid.ItemsSource = Trips; // send all the trips to tripsDataGrid
        }

        private void WireUpLists()
        {
            tripsDataGrid.ItemsSource = null;
            tripsDataGrid.Items.Clear();
            tripsDataGrid.ItemsSource = Trips;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //CallingWindow.UserPanelClosed();
        }

        private void selectTripBtn_Click(object sender, RoutedEventArgs e)
        {
            TripModel model = (TripModel)tripsDataGrid.SelectedItem;

            CreateSeatWindow createSeat = new CreateSeatWindow(this, model);
            this.Hide();
            createSeat.ShowDialog();
        }

        private void tripDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (tripDate.SelectedDate < DateTime.Now.Date)
            {
                MessageBox.Show("You can't choose an old date. ");
                tripDate.SelectedDate = selectedDate;
            }
            else
            {
                Trips = GlobalConfig.Connection.GetTrip_All((DateTime)tripDate.SelectedDate);
                selectedDate = (DateTime)tripDate.SelectedDate;
                WireUpLists();
            }
        }

        public void CreateSeatFormClosed()
        {
            this.Show();
        }

        public void SeatCreated(TripModel model, List<string> seatLogs)
        {
            Trips.Remove(Trips.Find(T => T.No == model.No));
            Trips.AddLast(model);

            GlobalConfig.Connection.Trip_InsertAll(Trips, selectedDate);

            GlobalConfig.Connection.WriteLogsToFile(seatLogs);

            WireUpLists();
        }
    }
}
