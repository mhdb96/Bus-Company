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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TASUI;
using TASUI.CreateForms;

namespace TASUI.UserControls
{
    /// <summary>
    /// Interaction logic for SeatUserControl.xaml
    /// </summary>
    public partial class SeatUserControl : UserControl
    {
        public SeatUserControl()
        {
            InitializeComponent();
        }

        private void seatStatus_Checked(object sender, RoutedEventArgs e)
        {
            CreateSeatWindow ParentWindow = (CreateSeatWindow)ParentFinder.FindParent<Window>(this);
            if (ParentWindow == null)
            {
                return; 
            }

            ParentWindow.AddPassengerInfo(seatNumber.Text);
        }


        private void seatStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            CreateSeatWindow ParentWindow = (CreateSeatWindow)ParentFinder.FindParent<Window>(this);
            if (ParentWindow == null)
            {
                return;
            }

            ParentWindow.RemovePassengerInfo(seatNumber.Text);
        }
       
    }
}
