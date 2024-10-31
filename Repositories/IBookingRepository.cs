﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingRepository
    {
        bool BookRoom(BookingReservation booking, BookingDetail bookingDetails);
        List<BookingReservation> GetAllBooking();
        List<BookingDetail> GetBookingDetails(int bookingReservationId);
    }
}
