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

namespace TASUI.Panels
{
    /// <summary>
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public IAdminPanelRequester CallingWindow;
        public AdminPanelWindow(IAdminPanelRequester caller)
        {
            InitializeComponent();
            CallingWindow = caller;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CallingWindow.AdminPanelClosed();
        }
    }
}
