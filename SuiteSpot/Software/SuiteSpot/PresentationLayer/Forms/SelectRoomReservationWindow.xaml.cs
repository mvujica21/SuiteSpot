using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System.Windows;

namespace PresentationLayer.Forms
{
    public partial class SelectRoomReservationWindow : Window
    {
        private readonly RoomReservationService _reservationService;

        public RoomReservation SelectedRoomReservation { get; private set; }

        public SelectRoomReservationWindow()
        {
            InitializeComponent();
            _reservationService = new RoomReservationService();
            LoadRoomReservations();
        }

        private async void LoadRoomReservations()
        {
            var roomReservations = await _reservationService.GetUnfinishedRoomReservationsAsync();
            RoomReservationsDataGrid.ItemsSource = roomReservations;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectedRoomReservation = RoomReservationsDataGrid.SelectedItem as RoomReservation;
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
