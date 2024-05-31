using PresentationLayer.Forms;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRoomReservation_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucRoomReservationNumGuests();

        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucManageEmployees();
        }

        private void BtnTaskManagment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRoomManagment_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucRoomManagment();

        }

        private void btnFacilities_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucManageFacilities();
        }

        private void btnWorkSchedule_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucManageWorkSchedule();
        }
    }
}
