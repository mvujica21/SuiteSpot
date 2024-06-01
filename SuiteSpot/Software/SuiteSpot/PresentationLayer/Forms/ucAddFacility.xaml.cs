using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucAddFacility.xaml
    /// </summary>
    public partial class ucAddFacility : UserControl
    {
        private FacilityService facilityService;
        private Facility currentFacility;

        public ucAddFacility()
        {
            InitializeComponent();
            facilityService = new FacilityService();
        }

        public ucAddFacility(Facility facility) : this()
        {
            currentFacility = facility;
            PopulateFacilityData(facility);
        }

        private void PopulateFacilityData(Facility facility)
        {
            if (facility != null)
            {
                txtFacilityName.Text = facility.Name;
                txtPrice.Text = facility.Price.ToString();
            }
        }

        private void btnSaveFacility_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string facilityName = txtFacilityName.Text;
                decimal price = decimal.Parse(txtPrice.Text);

                if (currentFacility == null)
                {
                    currentFacility = new Facility();
                }

                currentFacility.Name = facilityName;
                currentFacility.Price = price;

                if (currentFacility.Id == 0)
                {
                    Debug.WriteLine($"Adding facility: {facilityName}");
                    facilityService.AddFacility(currentFacility);
                }
                else
                {
                    Debug.WriteLine($"Updating facility: {facilityName}");
                    facilityService.UpdateFacility(currentFacility);
                }

                MessageBox.Show("Facility saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageFacilities();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving facility: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
