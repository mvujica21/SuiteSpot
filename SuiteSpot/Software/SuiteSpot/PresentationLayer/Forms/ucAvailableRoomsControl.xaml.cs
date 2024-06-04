using HotelManagement.Entities;
using BusinessLogicLayer.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucAvailableRoomsControl : UserControl
    {
        private int _roomCount;
        private int _guestCount;
        private readonly RoomService _roomService;
        private readonly RoomReservationService _reservationService;

        public ucAvailableRoomsControl(List<Room> availableRooms, int roomCount, int guestCount)
        {
            InitializeComponent();
            AvailableRoomsListView.ItemsSource = availableRooms;
            _roomCount = roomCount;
            _guestCount = guestCount;
            _roomService = new RoomService(); // Instantiate service here
            _reservationService = new RoomReservationService(); // Instantiate service here
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var checkInOutControl = new ucRoomReservationCheckInOut(_roomService, _reservationService, _roomCount, _guestCount);
            mainWindow.contentControl.Content = checkInOutControl;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableRoomsListView.SelectedItem is Room selectedRoom)
            {
                var guestEntryControl = new ucGuestEntry(selectedRoom, _guestCount);
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.contentControl.Content = guestEntryControl;
            }
            else
            {
                MessageBox.Show("Please select a room before proceeding.");
            }
        }
    }
}
