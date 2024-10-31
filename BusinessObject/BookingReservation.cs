using BusinessObject.Enums;
using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class BookingReservation
{
    public int BookingReservationId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? BookingStatus { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
    public BookingReservation(int bookingReservationID, DateOnly bookingDate, decimal totalPrice, int customerID,
            byte bookingStatus, Customer customer)
    {
        BookingReservationId = bookingReservationID;
        BookingDate = bookingDate;
        TotalPrice = totalPrice;
        CustomerId = customerID;
        BookingStatus = bookingStatus;
        Customer = customer;
    }

    public BookingReservation() { }

    public BookingReservation(int bookingReservationID, DateOnly bookingDate, decimal totalPrice, byte status, string customerFullName)
    {
        BookingReservationId = bookingReservationID;
        BookingDate = bookingDate;
        TotalPrice = totalPrice;
        BookingStatus = status;
        Customer customer = new Customer();
        customer.CustomerFullName = customerFullName;
    }

    public BookingReservation(int bookingReservationID, DateOnly bookingDate, decimal totalPrice, byte status, int customerId)
    {
        BookingReservationId = bookingReservationID;
        BookingDate = bookingDate;
        TotalPrice = totalPrice;
        BookingStatus = status;
        CustomerId = customerId;
    }
}
