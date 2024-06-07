using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucAddRoom.xaml
    /// </summary>
    public partial class ucAddRoom : UserControl
    {
        private RoomService roomService;
        private Room currentRoom;

        public ucAddRoom()
        {
            InitializeComponent();
            roomService = new RoomService();
        }

        public ucAddRoom(Room room) : this()
        {
            currentRoom = room;
            PopulateRoomData(room);
        }

        private void PopulateRoomData(Room room)
        {
            if (room != null)
            {
                PriceTextBox.Text = room.Price.ToString();
                RoomTypeComboBox.Text = room.Type;
                MaxCapacityTextBox.Text = room.maxxCapacity.ToString();
                NameTextBox.Text = room.Name;
                DescriptionTextBox.Text = room.Description;
                NumberTextBox.Text = room.Number.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal price = decimal.Parse(PriceTextBox.Text);
                string type = RoomTypeComboBox.Text;
                int maxCapacity = int.Parse(MaxCapacityTextBox.Text);
                string name = NameTextBox.Text;
                string description = DescriptionTextBox.Text;
                int number = int.Parse(NumberTextBox.Text);

                if (currentRoom == null)
                {
                    currentRoom = new Room();
                }

                currentRoom.Price = price;
                currentRoom.Type = type;
                currentRoom.maxxCapacity = maxCapacity;
                currentRoom.Name = name;
                currentRoom.Description = description;
                currentRoom.Number = number;

                if (currentRoom.Id == 0)
                {
                    roomService.AddRoom(currentRoom);
                }
                else
                {
                    roomService.UpdateRoom(currentRoom);
                }

                MessageBox.Show("Room saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucRoomManagment();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucRoomManagment();
        }
    }
}

