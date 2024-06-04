using HotelManagement.Entities;
using System.Collections.Generic;
using System.Windows;

namespace PresentationLayer.Forms
{
    public partial class SelectAmenityWindow : Window
    {
        public Facility SelectedAmenity { get; private set; }
        public int Amount { get; private set; }

        public SelectAmenityWindow(List<Facility> availableAmenities)
        {
            InitializeComponent();
            AmenityComboBox.ItemsSource = availableAmenities;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            SelectedAmenity = AmenityComboBox.SelectedItem as Facility;
            Amount = int.Parse(AmountTextBox.Text);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
