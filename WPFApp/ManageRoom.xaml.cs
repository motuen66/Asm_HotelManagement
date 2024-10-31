using BusinessObject;
using BusinessObject.Enums;
using Services;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for ManageRoom.xaml
    /// </summary>
    public partial class ManageRoom : Window
    {
        private readonly IRoomService _roomService;
        private RoomInformation _selectedRoom;

        public ManageRoom()
        {
            InitializeComponent();
            _roomService = new RoomService();
            LoadRooms();
        }


        private void LoadRooms()
        {
            List<RoomInformation> rooms = _roomService.GetAllRoom();
            RoomListBox.ItemsSource = rooms;
        }


        private void RoomListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RoomListBox.SelectedItem is RoomInformation room)
            {
                _selectedRoom = room;
                RoomNumberTextBox.Text = room.RoomNumber;
                DescriptionTextBox.Text = room.RoomDetailDescription;
                MaxCapacityTextBox.Text = room.RoomMaxCapacity.ToString();
                // Set RoomStatusComboBox based on RoomStatus value (1 for Active, 2 for Deleted)
                if (room.RoomStatus == 1)
                {
                    RoomStatusComboBox.SelectedIndex = 0; // Assuming first item is Active
                }
                else if (room.RoomStatus == 2)
                {
                    RoomStatusComboBox.SelectedIndex = 1; // Assuming second item is Deleted
                }
                PriceTextBox.Text = room.RoomPricePerDay.ToString();
            }
        }


        private void CreateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a Room Type.");
                return;
            }

            byte status = 0;
            if (RoomStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a Room Status.");
                return;
            }
            else if (RoomStatusComboBox.SelectedIndex == 0)
            {
                status = 1;
            }
            else
            {
                status = 2;
            }

            if (string.IsNullOrWhiteSpace(RoomNumberTextBox.Text) ||
            string.IsNullOrWhiteSpace(MaxCapacityTextBox.Text) ||
            string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            RoomInformation newRoom = new RoomInformation()
            {
                RoomNumber = RoomNumberTextBox.Text,
                RoomDetailDescription = DescriptionTextBox.Text,
                RoomMaxCapacity = int.Parse(MaxCapacityTextBox.Text),
                RoomStatus = status,
                RoomPricePerDay = decimal.Parse(PriceTextBox.Text),
                RoomTypeId = int.Parse((RoomTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString())

            };

            try
            {
                _roomService.CreateNewRoom(newRoom);
                MessageBox.Show("Created 1 new room");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadRooms();
        }


        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoom != null)
            {
                _selectedRoom.RoomNumber = RoomNumberTextBox.Text;
                _selectedRoom.RoomDetailDescription = DescriptionTextBox.Text;
                _selectedRoom.RoomMaxCapacity = int.Parse(MaxCapacityTextBox.Text);
                _selectedRoom.RoomStatus = (byte)RoomStatusComboBox.SelectedItem;
                _selectedRoom.RoomPricePerDay = decimal.Parse(PriceTextBox.Text);

                _roomService.UpdateRoom(_selectedRoom);
                LoadRooms(); 
            }
            else
            {
                MessageBox.Show("No room selected!");
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoom != null)
            {
                _roomService.DeleteRoom(_selectedRoom.RoomId);
                MessageBox.Show("Deleted 1 room.");
                LoadRooms();
            }
            else
            {
                MessageBox.Show("No room selected!");
            }
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            List<RoomInformation> filteredList = _roomService.GetAllRoom()
                .FindAll(r => r.RoomNumber.ToLower().Contains(searchText));

            RoomListBox.ItemsSource = filteredList;
        }
        private void clearForm()
        {
            RoomNumberTextBox.Text = "";
            DescriptionTextBox.Text = "";
            MaxCapacityTextBox.Text = "";
            RoomStatusComboBox.SelectedIndex = -1;
            PriceTextBox.Text = "";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

    }
}
