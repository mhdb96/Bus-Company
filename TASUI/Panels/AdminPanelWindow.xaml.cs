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

namespace TASUI.Panels
{
    /// <summary>
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window, ICreateTripRequester
    {
        public IAdminPanelRequester CallingWindow;
        public AdminPanelWindow(IAdminPanelRequester caller)
        {
            InitializeComponent();
            CallingWindow = caller;

            List<Trip> trips = new List<Trip>();
            trips.Add(new Trip() { No = 1, Destination = "Kocaeli - Ankara",   Date = new DateTime(2020, 3, 21), Plate = "34 IST 1453", Capacity = 25, Driver = "Talha AYDIN",     SeatPrice = 49 });
            trips.Add(new Trip() { No = 2, Destination = "Kocaeli - Izmir",    Date = new DateTime(2020, 3, 22), Plate = "58 SVS 1998", Capacity = 25, Driver = "Muhammed Bedavi", SeatPrice = 70 });
            trips.Add(new Trip() { No = 3, Destination = "Kocaeli - Istanbul", Date = new DateTime(2020, 3, 23), Plate = "41 KOU 2001", Capacity = 25, Driver = "Ali Karakuş",     SeatPrice = 110 });
            trips.Add(new Trip() { No = 4, Destination = "Kocaeli - Izmir", Date = new DateTime(2020, 3, 22), Plate = "58 SVS 1998", Capacity = 25, Driver = "Muhammed Bedavi", SeatPrice = 70 });
            trips.Add(new Trip() { No = 5, Destination = "Kocaeli - Istanbul", Date = new DateTime(2020, 3, 23), Plate = "41 KOU 2001", Capacity = 25, Driver = "Ali Karakuş", SeatPrice = 110 });
            trips.Add(new Trip() { No = 6, Destination = "Kocaeli - Izmir", Date = new DateTime(2020, 3, 22), Plate = "58 SVS 1998", Capacity = 25, Driver = "Muhammed Bedavi", SeatPrice = 70 });
            trips.Add(new Trip() { No = 7, Destination = "Kocaeli - Istanbul", Date = new DateTime(2020, 3, 23), Plate = "41 KOU 2001", Capacity = 25, Driver = "Ali Karakuş", SeatPrice = 110 });
            trips.Add(new Trip() { No = 8, Destination = "Kocaeli - Ankara", Date = new DateTime(2020, 3, 21), Plate = "34 IST 1453", Capacity = 25, Driver = "Talha AYDIN", SeatPrice = 49 });
            trips.Add(new Trip() { No = 9, Destination = "Kocaeli - Izmir", Date = new DateTime(2020, 3, 22), Plate = "58 SVS 1998", Capacity = 25, Driver = "Muhammed Bedavi", SeatPrice = 70 });
            trips.Add(new Trip() { No = 10, Destination = "Kocaeli - Istanbul", Date = new DateTime(2020, 3, 23), Plate = "41 KOU 2001", Capacity = 25, Driver = "Ali Karakuş", SeatPrice = 110 });

            dgUsers.ItemsSource = trips;
        }

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTripWindow createTrip = new CreateTripWindow(this);
            this.Hide();
            createTrip.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CallingWindow.AdminPanelClosed();
        }

        public void CreateTripFormClosed()
        {
            this.Show();
        }
    }

    public class Trip
    {
        public int No { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }

        public string Plate { get; set; }

        public int Capacity { get; set; }

        public string Driver { get; set; }

        public decimal SeatPrice { get; set; }
    }
}
