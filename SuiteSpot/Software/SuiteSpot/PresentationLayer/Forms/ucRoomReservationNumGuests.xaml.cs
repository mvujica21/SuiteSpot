using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucRoomReservationNumGuests.xaml
    /// </summary>
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
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucRoomReservationCheckInOut();

        }
    }
}
