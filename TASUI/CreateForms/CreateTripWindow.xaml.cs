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
using TASLibrary.Enums;

namespace TASUI.CreateForms
{
    /// <summary>
    /// Interaction logic for CreateTripWindow.xaml
    /// </summary>
    public partial class CreateTripWindow : Window
    {
        CLinkedList<DestinationModel> Destinations;
        CLinkedList<BusModel> Buses;
        CLinkedList<DriverModel> Drivers;

        public CreateTripWindow()
        {
            InitializeComponent();
            LoadListsData();
        }

        private void LoadListsData()
        {
            Destinations = DestinationModel.GetSampleData();
            destinationsCombobox.ItemsSource = Destinations;
            Buses = BusModel.GetSampleData();
            busesCombobox.ItemsSource = Buses;
            Drivers = DriverModel.GetSampleData();
            driversCombobox.ItemsSource = Drivers;
        }
    }
}
