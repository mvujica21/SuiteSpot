using BusinessLogicLayer.Services;
using HotelManagement.Entities;
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

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucManageFacilities.xaml
    /// </summary>
    public partial class ucManageFacilities : UserControl
    {
        public FacilityService facilityService;
        public List<Facility> facilities;
        public ucManageFacilities()
        {
            InitializeComponent();
            facilityService = new FacilityService();
            facilities = facilityService.GetFacilites();
            dgFacilities.ItemsSource = facilities;
        }

        private void btnAddFacility_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddFacility();
        }

        private void btnUpdateFacility_Click(object sender, RoutedEventArgs e)
        {
            Facility selectedFacility = dgFacilities.SelectedItem as Facility;
            if (selectedFacility != null)
            {
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddFacility(selectedFacility);
            }
            else
            {
                MessageBox.Show("Facility needs to be selected before update!", "Error!");
            }
        }

        private void btnDeleteFacility_Click(object sender, RoutedEventArgs e)
        {
            Facility selectedFacility = dgFacilities.SelectedItem as Facility;
            if (selectedFacility != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this facility?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    facilityService.DeleteFacility(selectedFacility);
                    (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();
                }
            }
            else
            {
                MessageBox.Show("Facility needs to be selected before delete!", "Error!");
            }
        }
    }
}
