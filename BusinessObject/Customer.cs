using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Customer
{
    public Customer()
    {
    }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public string? Telephone { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateOnly? CustomerBirthday { get; set; }

    public byte? CustomerStatus { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();

    public Customer(int customerId, string? customerFullName, string? telephone, string emailAddress, DateOnly? customerBirthday, byte? customerStatus, string? password, ICollection<BookingReservation> bookingReservations)
    {
        CustomerId = customerId;
        CustomerFullName = customerFullName;
        Telephone = telephone;
        EmailAddress = emailAddress;
        CustomerBirthday = customerBirthday;
        CustomerStatus = customerStatus;
        Password = password;
        BookingReservations = bookingReservations;
    }

    public Customer(int customerId, string? customerFullName, string? telephone, string emailAddress, DateOnly? customerBirthday, byte? customerStatus, ICollection<BookingReservation> bookingReservations)
    {
        CustomerId = customerId;
        CustomerFullName = customerFullName;
        Telephone = telephone;
        EmailAddress = emailAddress;
        CustomerBirthday = customerBirthday;
        CustomerStatus = customerStatus;
        BookingReservations = bookingReservations;
    }

    public Customer(int customerID, string? customerFullName, string? telephone, string? emailAddress, DateOnly customerBirthday, byte customerStatus, string? password)
    {
        CustomerId = customerID;
        CustomerFullName = customerFullName;
        Telephone = telephone;
        EmailAddress = emailAddress;
        CustomerBirthday = customerBirthday;
        CustomerStatus = customerStatus;
        Password = password;
    }

    public override string? ToString()
    {
        return $"CustomerId: {this.CustomerId}; CustomerName: {this.CustomerFullName}; CustomerPhone: {this.Telephone}; Email: {this.EmailAddress}";
    }
}
