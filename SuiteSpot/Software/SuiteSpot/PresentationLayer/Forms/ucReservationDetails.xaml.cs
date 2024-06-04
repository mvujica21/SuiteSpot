using HotelManagement.Entities;
using BusinessLogicLayer.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace PresentationLayer.Forms
{
    public partial class ucReservationDetails : UserControl
    {
        private RoomReservation _selectedReservation;
        private List<Facility> _facilities;
        private readonly BillService _billService;

        public ucReservationDetails(RoomReservation selectedReservation)
        {
            InitializeComponent();
            _selectedReservation = selectedReservation;
            _billService = new BillService();
            LoadReservationDetails();
            LoadFacilities();
        }

        private void LoadReservationDetails()
        {
            RoomNumberTextBlock.Text = _selectedReservation.Room.Number.ToString();
            GuestNameTextBlock.Text = $"{_selectedReservation.Guest.FirstName} {_selectedReservation.Guest.LastName}";
            StartDateTextBlock.Text = _selectedReservation.StartDate.ToShortDateString();
            EndDateTextBlock.Text = _selectedReservation.EndDate.ToShortDateString();
            FacilitiesListView.ItemsSource = _selectedReservation.Bills.SelectMany(b => b.FacilityBillings).ToList();
        }

        private async void LoadFacilities()
        {
            var facilityService = new FacilityService();
            _facilities = await facilityService.GetFacilitiesAsync(); // Assuming an async method to get facilities
        }

        private void AddFacility_Click(object sender, RoutedEventArgs e)
        {
            // Show a dialog or new control to select a facility and amount
            // For simplicity, here we assume a facility is added directly

            if (_facilities == null || !_facilities.Any())
            {
                MessageBox.Show("No facilities available to add.");
                return;
            }

            var selectedFacility = _facilities.First(); // Replace with actual selection logic
            var amount = 1; // Replace with actual amount logic

            var bill = _selectedReservation.Bills.FirstOrDefault();
            if (bill == null)
            {
                bill = new Bill
                {
                    RoomReservationId = _selectedReservation.Id,
                    Date = DateTime.Now,
                    FacilityBillings = new List<FacilityBilling>()
                };
                _selectedReservation.Bills.Add(bill);
            }

            var newFacilityBilling = new FacilityBilling
            {
                Bill = bill,
                Facility = selectedFacility,
                Amount = amount
            };

            bill.FacilityBillings.Add(newFacilityBilling);
            FacilitiesListView.ItemsSource = null;
            FacilitiesListView.ItemsSource = _selectedReservation.Bills.SelectMany(b => b.FacilityBillings).ToList(); // Refresh the list
        }

        private async void FinishReservation_Click(object sender, RoutedEventArgs e)
        {
            // Logic to finalize the reservation, e.g., save the updated Bill and FacilityBillings to the database
            await _billService.SaveBillAsync(_selectedReservation.Bills.First());

            MessageBox.Show("Reservation finished and bill generated successfully.");
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = new ucBills(); // Navigate back to the bills overview
        }
    }
}
