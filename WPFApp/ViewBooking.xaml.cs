using BusinessObject;
using BusinessObject.Enums;
using BusinessObject.Models;
using DataAccessLayer;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace WPFApp
{
    public partial class ManageBooking : Window
    {
        private List<BookingReservation> bookings;
        private readonly BookingService _bookingService;

        public ManageBooking()
        {
            InitializeComponent();
            _bookingService = new BookingService();
            LoadBookings();
        }

        private void LoadBookings()
        {
            bookings = _bookingService.GetALlBooking();
            BookingListBox.ItemsSource = bookings;
        }

        private void BookingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingListBox.SelectedItem is BookingReservation selectedBooking)
            {
                BookingDatePicker.SelectedDate = selectedBooking.BookingDate.HasValue ? selectedBooking.BookingDate.Value.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null;
                TotalPriceTextBox.Text = selectedBooking.TotalPrice.ToString();
                CustomerIDTextBox.Text = selectedBooking.CustomerId.ToString();
                // Determine booking status
                string statusToSelect;
                if (selectedBooking.BookingStatus == 1)
                {
                    BookingStatusComboBox.SelectedIndex = 1;
                }
                else
                {
                    BookingStatusComboBox.SelectedIndex = 0;
                }
                //RoomIDComboBox.SelectedIndex = RoomIDComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == selectedBooking)
                //BookingStatusComboBox.SelectedItem = BookingStatusComboBox.Items
                //    .Cast<ComboBoxItem>()
                //    .FirstOrDefault(item => item.Content.ToString() == statusToSelect);

            }
        }

        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingReservation newBooking = new BookingReservation()
            {
                BookingReservationId = int.Parse(BookingReservationIDTextBox.Text),
                BookingDate = DateOnly.FromDateTime(BookingDatePicker.SelectedDate.Value),
                TotalPrice = decimal.Parse(TotalPriceTextBox.Text),
                CustomerId = int.Parse(CustomerIDTextBox.Text),
                BookingStatus = byte.Parse((BookingStatusComboBox.SelectedItem as ComboBoxItem).Content.ToString())
            };

            BookingDetail newBookingDetail = new BookingDetail()
            {
                BookingReservationId = int.Parse(BookingReservationIDTextBox.Text),
                RoomId = int.Parse((RoomIDComboBox.SelectedItem as ComboBoxItem).Content.ToString()),
                StartDate = DateOnly.FromDateTime(BookingDatePicker.SelectedDate.Value),
                ActualPrice = decimal.Parse(ActualPriceTextBox.Text)
            };

            bool isSuccess = _bookingService.BookRoom(newBooking, newBookingDetail);

            if (isSuccess)
            {
                MessageBox.Show("Booking created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Booking creation failed. Please check the details and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ClearForm();
            LoadBookings();
        }

        private void ClearForm()
        {
            BookingDatePicker.SelectedDate = null;
            TotalPriceTextBox.Clear();
            CustomerIDTextBox.Clear();
            BookingStatusComboBox.SelectedIndex = -1;
            BookingListBox.SelectedItem = null;
        }
        private void OpenDetailsPopup_Click(object sender, RoutedEventArgs e)
        {
            if (BookingListBox.SelectedItem is BookingReservation selectedBooking)
            {
                var bookingDetails = _bookingService.GetBookingDetails(selectedBooking.BookingReservationId);
                BookingDetailsPopup.DataContext = bookingDetails;
                //BookingDetailsItem.DataContext = bookingDetails;
                BookingDetailsPopup.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Please select a booking reservation.");
            }
        }
        private void CloseDetailsPopup_Click(object sender, RoutedEventArgs e)
        {
            BookingDetailsPopup.IsOpen = false; 
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
    }
}
