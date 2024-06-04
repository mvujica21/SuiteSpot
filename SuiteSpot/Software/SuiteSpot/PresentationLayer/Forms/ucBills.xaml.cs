using HotelManagement.Entities;
using BusinessLogicLayer.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucBills : UserControl
    {
        private readonly RoomReservationService _reservationService;

        public ucBills()
        {
            InitializeComponent();
            _reservationService = new RoomReservationService();
            LoadRoomReservations();
        }

        private async void LoadRoomReservations()
        {
            var roomReservations = await _reservationService.GetUnfinishedRoomReservationsAsync();
            RoomReservationsListView.ItemsSource = roomReservations;
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (RoomReservationsListView.SelectedItem is RoomReservation selectedReservation)
            {
                var billDetailsControl = new ucBillDetails(selectedReservation);
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.contentControl.Content = billDetailsControl;
            }
            else
            {
                MessageBox.Show("Please select a room reservation before proceeding.");
            }
        }
    }
}
