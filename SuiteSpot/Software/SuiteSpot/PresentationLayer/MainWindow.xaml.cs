using PresentationLayer.Forms;
using System.Windows;

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

            CheckUserRole(SessionManager.UserRole);
        }

        public void CheckUserRole(string userRole)
        {
            if (userRole == "Manager")
            {
                btnBills.Visibility = Visibility.Visible;
                btnRoomReservation.Visibility = Visibility.Visible;
                btnManageEmployees.Visibility = Visibility.Visible;
                btnRoomManagment.Visibility = Visibility.Visible;
                btnFacilities.Visibility = Visibility.Visible;
                btnWorkSchedule.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Receptionist")
            {
                btnBills.Visibility = Visibility.Visible;
                btnRoomReservation.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Visible;
                btnManageEmployees.Visibility = Visibility.Collapsed;
                btnRoomManagment.Visibility = Visibility.Collapsed;
                btnFacilities.Visibility = Visibility.Collapsed;
                btnWorkSchedule.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Collapsed;
            }
            else if (userRole == null)
            {
                btnBills.Visibility = Visibility.Collapsed;
                btnRoomReservation.Visibility = Visibility.Collapsed;
                btnManageEmployees.Visibility = Visibility.Collapsed;
                btnRoomManagment.Visibility = Visibility.Collapsed;
                btnFacilities.Visibility = Visibility.Collapsed;
                btnWorkSchedule.Visibility = Visibility.Collapsed;
                btnLogout.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Visible;
            }
        }

        private void btnRoomReservation_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucRoomReservationNumGuests();
        }

        private void btnManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucManageEmployees();
        }


        private void btnRoomManagement_Click(object sender, RoutedEventArgs e)
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucLogin();
        }

        private void btnBills_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ucBills();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.EmployeeId = 0;
            SessionManager.UserRole = null;
            contentControl.Content = null;
            CheckUserRole(SessionManager.UserRole);
            MessageBox.Show("You are succesfully logged out.");
        }
    }
}
