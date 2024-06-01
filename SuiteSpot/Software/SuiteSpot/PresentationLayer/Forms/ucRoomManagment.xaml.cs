using BusinessLogicLayer.Services;
using HotelManagement.Entities;
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
    /// Interaction logic for ucEditRooms.xaml
    /// </summary>
    public partial class ucRoomManagment : UserControl
    {
        private RoomService roomService;
        private List<Room> allRooms;

        public ucRoomManagment()
        {
            InitializeComponent();
            roomService = new RoomService();
            LoadRooms();

        }
        private void LoadRooms()
        {
            allRooms = roomService.GetRooms();
            dgRooms.ItemsSource = allRooms;

        }

        private void cbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterRooms();

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRooms();
        }
        private void FilterRooms()
        {
            string roomType = (cbRoomType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string searchText = tbSearch.Text.ToLower();

            var filteredRooms = allRooms.Where(r =>
                (string.IsNullOrEmpty(roomType) || roomType == "All" || r.Type == roomType) &&
                (string.IsNullOrEmpty(searchText) || r.Name.ToLower().Contains(searchText) || r.Description.ToLower().Contains(searchText))
            ).ToList();

            dgRooms.ItemsSource = filteredRooms;
        }
        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddRoom();
        }

        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = dgRooms.SelectedItem as Room;
            if (selectedRoom != null)
            {
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddRoom(selectedRoom);
            }
            else
            {
                MessageBox.Show("Room needs to be selected before update!", "Error!");
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = dgRooms.SelectedItem as Room;
            if (selectedRoom != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this role?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    roomService.DeleteRoom(selectedRoom);
                    (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucRoomManagment();
                }
            }
            else
            {
                MessageBox.Show("Employee needs to be selected before delete!", "Error!");
            }
        }
    }
}
