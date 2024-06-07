using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucBillDetails : UserControl
    {
        private Bill _selectedBill;
        private List<Facility> _availableAmenities;
        private readonly BillService _billService;
        private readonly FacilityService _facilityService;
        private readonly RoomReservationService _reservationService;

        public ucBillDetails(Bill selectedBill)
        {
            InitializeComponent();
            _selectedBill = selectedBill;
            _billService = new BillService();
            _facilityService = new FacilityService();
            _reservationService = new RoomReservationService();
            LoadBillDetails();
            LoadAmenities();
        }

        private async void LoadBillDetails()
        {
            BillIdTextBlock.Text = _selectedBill.Id.ToString();
            DateTextBlock.Text = _selectedBill.Date.ToString("g");

            if (_selectedBill.RoomReservationId.HasValue && _selectedBill.RoomReservationId.Value != 0)
            {
                var roomReservation = await _reservationService.GetRoomReservationAsync(_selectedBill.RoomReservationId.Value);
                RoomReservationsListView.ItemsSource = new List<RoomReservation> { roomReservation };
            }
            else
            {
                RoomReservationsListView.ItemsSource = new List<RoomReservation>();
            }

            var facilityBillings = await _billService.GetFacilityBillingsByBillIdAsync(_selectedBill.Id);
            _selectedBill.FacilityBillings = facilityBillings;

            AmenitiesListView.ItemsSource = _selectedBill.FacilityBillings.Select(fb => new
            {
                fb.Facility.Name,
                fb.Amount,
                fb.Facility.Price
            }).ToList();

            UpdateTotalPrice();
        }

        private async void LoadAmenities()
        {
            _availableAmenities = await _facilityService.GetAvailableAmenitiesAsync();
        }

        private async void AddAmenity_Click(object sender, RoutedEventArgs e)
        {
            var selectAmenityWindow = new SelectAmenityWindow(_availableAmenities);
            if (selectAmenityWindow.ShowDialog() == true)
            {
                var selectedAmenity = selectAmenityWindow.SelectedAmenity;
                var amount = selectAmenityWindow.Amount;

                var existingFacilityBilling = _selectedBill.FacilityBillings
                    .FirstOrDefault(fb => fb.FacilityId == selectedAmenity.Id);

                if (existingFacilityBilling != null)
                {
                    existingFacilityBilling.Amount += amount;
                    await _billService.SaveFacilityBillingAsync(existingFacilityBilling);
                }
                else
                {
                    var facilityBilling = new FacilityBilling
                    {
                        FacilityId = selectedAmenity.Id,
                        Amount = amount,
                        BillId = _selectedBill.Id,
                    };

                    if (_selectedBill.FacilityBillings == null)
                    {
                        _selectedBill.FacilityBillings = new List<FacilityBilling>();
                    }

                    _selectedBill.FacilityBillings.Add(facilityBilling);
                    await _billService.SaveFacilityBillingAsync(facilityBilling);
                }

                UpdateTotalPrice();
                LoadBillDetails();
            }
        }

        private async void FinalizeBill_Click(object sender, RoutedEventArgs e)
        {
            _selectedBill.Price = _selectedBill.FacilityBillings.Sum(fb => fb.Facility.Price * fb.Amount);
            _selectedBill.Status = "Finalized";
            await _billService.FinalizeBillAsync(_selectedBill);
            if (_selectedBill.RoomReservationId.HasValue && _selectedBill.RoomReservationId.Value != 0)
            {
                var roomReservation = await _reservationService.GetRoomReservationAsync(_selectedBill.RoomReservationId.Value);
                roomReservation.Status = "Finished";
                await _reservationService.UpdateRoomReservationAsync(roomReservation);
            }

            MessageBox.Show("Bill finalized.");
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = new ucBills();
        }

        private async void UpdateTotalPrice()
        {
            var totalPrice = 0m;

            if (_selectedBill.FacilityBillings != null)
            {
                foreach (var fb in _selectedBill.FacilityBillings)
                {
                    var facility = await _facilityService.GetFacilityByIdAsync(fb.FacilityId);
                    totalPrice += facility.Price * fb.Amount;
                }
            }

            if (_selectedBill.RoomReservationId.HasValue && _selectedBill.RoomReservationId.Value != 0)
            {
                var roomReservation = await _reservationService.GetRoomReservationAsync(_selectedBill.RoomReservationId.Value);
                totalPrice += roomReservation.Room.Price;
            }

            TotalPriceTextBlock.Text = totalPrice.ToString("C");
            _selectedBill.Price = totalPrice;
            await _billService.UpdateBillAsync(_selectedBill);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = new ucBills();
        }
    }
}
