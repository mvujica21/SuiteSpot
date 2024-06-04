using HotelManagement.Entities;
using BusinessLogicLayer.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucBillDetails : UserControl
    {
        private RoomReservation _selectedReservation;
        private Bill _bill;
        private List<Facility> _availableAmenities;
        private readonly RoomReservationService _reservationService;
        private readonly BillService _billService;
        private readonly FacilityService _facilityService;

        public ucBillDetails(RoomReservation selectedReservation)
        {
            InitializeComponent();
            _selectedReservation = selectedReservation;
            _reservationService = new RoomReservationService();
            _billService = new BillService();
            _facilityService = new FacilityService();
            InitializeBill();
            LoadAmenities();
            LoadReservationDetails();
        }

        private async void InitializeBill()
        {
            _bill = await _billService.GetOrCreateBillAsync(_selectedReservation.Id);
            if (_bill.FacilityBillings == null)
            {
                _bill.FacilityBillings = new List<FacilityBilling>();
            }
            UpdateTotalPrice();
        }

        private async void LoadAmenities()
        {
            _availableAmenities = await _facilityService.GetAvailableAmenitiesAsync();
        }

        private void LoadReservationDetails()
        {
            RoomNumberTextBlock.Text = _selectedReservation.Room.Number.ToString();
            GuestNameTextBlock.Text = $"{_selectedReservation.Guest.FirstName} {_selectedReservation.Guest.LastName}";
            StartDateTextBlock.Text = _selectedReservation.StartDate.ToShortDateString();
            EndDateTextBlock.Text = _selectedReservation.EndDate.ToShortDateString();
            AmenitiesListView.ItemsSource = _bill.FacilityBillings.Select(fb => fb.Facility).ToList();
        }

        private void AddAmenity_Click(object sender, RoutedEventArgs e)
        {
            var selectAmenityWindow = new SelectAmenityWindow(_availableAmenities);
            if (selectAmenityWindow.ShowDialog() == true)
            {
                var selectedAmenity = selectAmenityWindow.SelectedAmenity;
                var amount = selectAmenityWindow.Amount;

                var facilityBilling = new FacilityBilling
                {
                    FacilityId = selectedAmenity.Id,
                    Facility = selectedAmenity,
                    Amount = amount,
                    BillId = _bill.Id,
                    Bill = _bill
                };

                _bill.FacilityBillings.Add(facilityBilling);
                UpdateTotalPrice();
                LoadReservationDetails();
            }
        }

        private async void FinalizeBill_Click(object sender, RoutedEventArgs e)
        {
            _bill.Price = _bill.FacilityBillings.Sum(fb => fb.Facility.Price * fb.Amount);
            await _billService.FinalizeBillAsync(_bill);
            _selectedReservation.Status = "Finished";
            await _reservationService.UpdateRoomReservationAsync(_selectedReservation);

            MessageBox.Show("Bill finalized and room reservation completed.");
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = new ucBills(); // Navigate back to the bills overview
        }

        private void UpdateTotalPrice()
        {
            var totalPrice = _bill.FacilityBillings.Sum(fb => fb.Facility.Price * fb.Amount);
            TotalPriceTextBlock.Text = totalPrice.ToString("C");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = new ucBills();
        }
    }
}
