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
using TASLibrary.Enums;
using TASLibrary.Models;
using TASUI.Requesters;
using TASUI.UserControls;

namespace TASUI.CreateForms
{
    /// <summary>
    /// Interaction logic for CreateSeatWindow.xaml
    /// </summary>
    public partial class CreateSeatWindow : Window
    {
        TripModel Trip;
        ICreateSeatRequester CallingWindow;
        public CreateSeatWindow(ICreateSeatRequester caller, TripModel trip)
        {
            InitializeComponent();

            CallingWindow = caller;

            Trip = trip;

            CreateGridDefinition();

            CreateSeatMatris();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CallingWindow.CreateSeatFormClosed();
        }
        public void AddPassengerInfo(string seatNumber)
        {
            PassengerInfoUserControl passenger = new PassengerInfoUserControl();

            passenger.seatNumber.Text = seatNumber;

            passengerStackPanel.Children.Add(passenger);
        }
        public void RemovePassengerInfo(string seatNumber)
        {
            foreach (PassengerInfoUserControl passenger in passengerStackPanel.Children)
            {
                if (passenger.seatNumber.Text == seatNumber)
                {
                    // remove from ui
                    passengerStackPanel.Children.Remove(passenger);

                    // reset seat info
                    SeatModel seat = Trip.Seats.Find(S => S.No.ToString() == seatNumber);
                    seat.Status = SeatStatus.Empty;
                    seat.Passenger = new PassengerModel();

                    break;
                }
            }
        }
        private void CreateGridDefinition()
        {
            foreach (SeatModel seat in Trip.Seats)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Tag = seat;
                cd.Width = new GridLength();
                seatsGrid.ColumnDefinitions.Add(cd);
            }
        }
        private void CreateSeatMatris()
        {
            int i = 0, j = 0; 
            foreach (SeatModel seat in Trip.Seats)
            {                
                if(i % 5 == 2 )
                {
                    i++;
                }
                int mod = i % 5;

                SeatUserControl seatUser = new SeatUserControl();

                seatUser.seatNumber.Text = seat.No.ToString();
                if (seat.Status == SeatStatus.Sold || seat.Status == SeatStatus.Reserved)
                {
                    seatUser.seatStatus.IsChecked = true;

                    PassengerInfoUserControl passenger = new PassengerInfoUserControl();

                    passenger.seatNumber.Text = seat.No.ToString();
                    passenger.passengerNameTextBox.Text = seat.Passenger.Name;
                    passenger.passengerGenderComboBox.SelectedIndex = seat.Passenger.Sex == SexType.Male ? 0 : 1;
                    passenger.seatStatusComboBox.SelectedIndex = seat.Status == SeatStatus.Sold ? 0 : 1;

                    passengerStackPanel.Children.Add(passenger);
                }

                seatUser.Margin = new Thickness(10);
                Grid.SetRow(seatUser, mod);
                Grid.SetColumn(seatUser, j);

                seatsGrid.Children.Add(seatUser);

                
                
                i++;
                if (i % 5 == 0)
                {
                    j++;
                }

            }
        }

        private void confirmPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (PassengerInfoUserControl passenger in passengerStackPanel.Children)
            {
                foreach (SeatModel seat in Trip.Seats)
                {
                    if (seat.No.ToString() == passenger.seatNumber.Text)
                    {
                        // seat status
                        if (passenger.seatStatusComboBox.SelectedIndex == 0)
                        {
                            seat.Status = SeatStatus.Sold;
                        }
                        else if (passenger.seatStatusComboBox.SelectedIndex == 1)
                        {
                            seat.Status = SeatStatus.Reserved;
                        }

                        // passenger gender
                        if (passenger.passengerGenderComboBox.SelectedIndex == 0)
                        {
                            seat.Passenger.Sex = SexType.Male;
                        }
                        else if (passenger.seatStatusComboBox.SelectedIndex == 1)
                        {
                            seat.Passenger.Sex = SexType.Female;
                        }

                        seat.Passenger.Name = passenger.passengerNameTextBox.Text;

                        break;
                    }
                }
            }
                        
            CallingWindow.SeatCreated(Trip);
            this.Close();
        }
    }
}
