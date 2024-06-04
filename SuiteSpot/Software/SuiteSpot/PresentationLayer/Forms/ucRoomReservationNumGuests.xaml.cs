using BusinessLogicLayer.Services;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucRoomReservationNumGuests : UserControl
    {
        public ucRoomReservationNumGuests()
        {
            InitializeComponent();
            numRoomsComboBox.SelectedIndex = 0; // Default to 1 room
            numGuestsComboBox.SelectedIndex = 0; // Default to 1 guest
        }

        public int SelectedRoomCount
        {
            get
            {
                if (numRoomsComboBox.SelectedItem is ComboBoxItem item)
                {
                    return int.Parse(item.Content.ToString());
                }
                return 1; // Default to 1 room if nothing is selected or in case of error
            }
        }

        public int SelectedGuestCount
        {
            get
            {
                if (numGuestsComboBox.SelectedItem is ComboBoxItem item)
                {
                    return int.Parse(item.Content.ToString());
                }
                return 1; // Default to 1 guest if nothing is selected or in case of error
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var roomCount = SelectedRoomCount;
            var guestCount = SelectedGuestCount;

            var roomService = new RoomService();
            var reservationService = new RoomReservationService();

            var checkInOutControl = new ucRoomReservationCheckInOut(roomService, reservationService, roomCount, guestCount);
            (Window.GetWindow(this) as MainWindow).contentControl.Content = checkInOutControl;
        }
    }
}
