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
using TASLibrary.Models;
using TASUI.Panels;
using TASUI.Requesters;

namespace TASUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAdminPanelRequester, IUserPanelRequester
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserPanelWindow user = new UserPanelWindow(/*this*/);
            this.Hide();
            user.ShowDialog();
        }

        private void AdminLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow admin = new AdminPanelWindow(/*this*/);
            this.Hide();
            admin.ShowDialog();
        }

        public void UserPanelClosed()
        {
            this.Show();
        }

        public void AdminPanelClosed()
        {
            this.Show();
        }

        public void editTrip(TripModel trip)
        {
            throw new NotImplementedException();
        }
    }
}
