using HotelManagement.Entities;
using BusinessLogicLayer.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;

namespace PresentationLayer.Forms
{
    public partial class ucGuestEntry : UserControl
    {
        private Room _selectedRoom;
        private readonly RoomReservationService _reservationService;
        private readonly BillService _billService;
        private readonly List<Guest> _guests = new List<Guest>();
        private readonly int _totalGuests;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public ucGuestEntry(Room selectedRoom, int totalGuests, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            _selectedRoom = selectedRoom;
            _reservationService = new RoomReservationService();
            _billService = new BillService();
            _totalGuests = totalGuests;
            _startDate = startDate;
            _endDate = endDate;
            UpdateGuestCounter();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var availableRoomsControl = new ucAvailableRoomsControl(new List<Room>(), 0, 0, _startDate, _endDate);
            mainWindow.contentControl.Content = availableRoomsControl;
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            var guest = new Guest
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text
            };

            _guests.Add(guest);
            ClearGuestForm();

            if (_guests.Count < _totalGuests)
            {
                UpdateGuestCounter();
            }
            else
            {
                var roomReservation = await _reservationService.CreateRoomReservationAsync(_selectedRoom, _guests, _startDate, _endDate, SessionManager.EmployeeId);

                MessageBox.Show("Guest information and room reservation saved successfully!");
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.contentControl.Content = new ucRoomReservationNumGuests();
            }
        }

        private void ClearGuestForm()
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
        }

        private void UpdateGuestCounter()
        {
            GuestCounterTextBlock.Text = $"Entering guest {_guests.Count + 1} of {_totalGuests}";
        }
    }
}
