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

namespace TASUI.Panels
{
    /// <summary>
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window, ICreateTripRequester
    {
        public IAdminPanelRequester CallingWindow;
        CLinkedList<TripModel> Trips;
        //List<TripModel> TripList = new List<TripModel>();
        public AdminPanelWindow(/*IAdminPanelRequester caller*/)
        {
            InitializeComponent();
            //CallingWindow = caller;

            Trips = TripModel.GetSampleData();
            //test();
            dgUsers.ItemsSource = Trips;

        }

        private void WireUpLists()
        {
            dgUsers.ItemsSource = null;
            dgUsers.Items.Clear();
            dgUsers.ItemsSource = Trips;
        }

        //private void test()
        //{
        //    TripModel model = new TripModel();
        //    model.No = 22;
        //    model.Destination.Name = "Kocaeli";
        //    model.Bus.Capacity = 10;
        //    model.Bus.Plate = "ASD1234";
        //    model.Driver.Name = "Ahmet";
        //    model.Date = DateTime.Now;
        //    model.Seats.AddLast(new SeatModel(1, new PassengerModel("muhammed", SexType.Male), SeatStatus.Sold));
        //    TripModel test = new TripModel();
        //    test.No = 22;
        //    test.Destination.Name = "Locaeli";
        //    test.Bus.Capacity = 17;
        //    test.Bus.Plate = "ASD1234";
        //    test.Driver.Name = "Ahmet";
        //    test.Date = DateTime.Now;
        //    test.Seats.AddLast(new SeatModel(1, new PassengerModel("Ahmad", SexType.Male), SeatStatus.Sold));
        //    TripList.Add(model);
        //    TripList.Add(test);
        //}

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTripWindow createTrip = new CreateTripWindow(this);
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
            Trips.AddLast(model);
            WireUpLists();
        }
    }
}
