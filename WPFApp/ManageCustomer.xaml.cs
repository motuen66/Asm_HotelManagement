﻿using BusinessObject;
using BusinessObject.Enums;
using Services;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICustomerService _customerService;
        private Customer _selectedCustomer;

        public MainWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService(); // Use DI in the future, this is for simplicity now
            LoadCustomers();
        }

        // Load all customers into the listbox
        private void LoadCustomers()
        {
            List<Customer> customers = _customerService.GetAllCustomer();
            CustomerListBox.ItemsSource = customers;
        }

        private void CustomerListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CustomerListBox.SelectedItem is Customer customer)
            {
                _selectedCustomer = customer;
                FullNameTextBox.Text = customer.CustomerFullName;
                TelephoneTextBox.Text = customer.Telephone;
                EmailTextBox.Text = customer.EmailAddress;
                PasswordBox.Password = customer.Password;
                if (customer.CustomerBirthday.HasValue)
                {
                    // Convert DateOnly to DateTime and assign it to the DatePicker
                    BirthdayPicker.SelectedDate = customer.CustomerBirthday.Value.ToDateTime(TimeOnly.MinValue);
                }
                else
                {
                    // Handle case where CustomerBirthday is null
                    BirthdayPicker.SelectedDate = null;
                }

            }
        }

        // Create new customer
        private void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            Customer newCustomer = new Customer()
            {
                CustomerFullName = FullNameTextBox.Text,
                Telephone = TelephoneTextBox.Text,
                EmailAddress = EmailTextBox.Text,
                CustomerBirthday = BirthdayPicker.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(BirthdayPicker.SelectedDate.Value)
                        : DateOnly.FromDateTime(DateTime.Now),
                CustomerStatus = 1,
                Password = password
            };

            _customerService.CreateCustomer(newCustomer);
            MessageBox.Show("Created 1 customer.");
            LoadCustomers(); // Reload the customer list
        }

        // Update selected customer
        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer != null)
            {
                _selectedCustomer.CustomerFullName = FullNameTextBox.Text;
                _selectedCustomer.Telephone = TelephoneTextBox.Text;
                _selectedCustomer.EmailAddress = EmailTextBox.Text;
                _selectedCustomer.CustomerBirthday = BirthdayPicker.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(BirthdayPicker.SelectedDate.Value)
                        : DateOnly.FromDateTime(DateTime.Now);
                _customerService.UpdateCustomer(_selectedCustomer);
                MessageBox.Show("Updated 1 customer.");
                LoadCustomers(); // Reload the customer list
            }
            else
            {
                MessageBox.Show("No customer selected!");
            }
        }

        // Delete selected customer
        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer != null)
            {
                _customerService.DeleteCustomer(_selectedCustomer.CustomerId);
                MessageBox.Show("Deleted 1 customer.");
                LoadCustomers(); // Reload the customer list
            }
            else
            {
                MessageBox.Show("No customer selected!");
            }
        }

        // Search customers by name
        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            List<Customer> filteredList = _customerService.GetAllCustomer()
                .FindAll(c => c.CustomerFullName.ToLower().Contains(searchText));

            CustomerListBox.ItemsSource = filteredList;
        }
        private void clearForm()
        {
            FullNameTextBox.Text = "";
            TelephoneTextBox.Text = "";
            BirthdayPicker.Text = "";
            EmailTextBox.Text = "";
            PasswordBox.Password = "";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }
    }
}