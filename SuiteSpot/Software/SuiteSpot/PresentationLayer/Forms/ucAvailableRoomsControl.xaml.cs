using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucAvailableRoomsControl : UserControl
    {
        private int _roomCount;
        private int _guestCount;
        private DateTime _checkInDate;
        private DateTime _checkOutDate;

        public ucAvailableRoomsControl(List<Room> availableRooms, int roomCount, int guestCount, DateTime checkInDate, DateTime checkOutDate)
        {
            InitializeComponent();
            AvailableRoomsListView.ItemsSource = availableRooms;
            _roomCount = roomCount;
            _guestCount = guestCount;
            _checkInDate = checkInDate;
            _checkOutDate = checkOutDate;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (Window.GetWindow(this) as MainWindow);
            var checkInOutControl = new ucRoomReservationCheckInOut(new RoomService(), new RoomReservationService(), _roomCount, _guestCount, _checkInDate, _checkOutDate);
            mainWindow.contentControl.Content = checkInOutControl;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableRoomsListView.SelectedItem is Room selectedRoom)
            {
                var guestEntryControl = new ucGuestEntry(selectedRoom, _guestCount, _checkInDate, _checkOutDate);
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
