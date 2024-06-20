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
            numRoomsComboBox.SelectedIndex = 0; 
            numGuestsComboBox.SelectedIndex = 0; 
        }

        public int SelectedRoomCount
        {
            get
            {
                if (numRoomsComboBox.SelectedItem is ComboBoxItem item)
                {
                    return int.Parse(item.Content.ToString());
                }
                return 1; 
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
                return 1;
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
