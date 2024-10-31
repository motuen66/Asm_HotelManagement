using BusinessObject;
using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingDAO
    {
        private static string SELECT_ALL_BOOKING = "SELECT * FROM [dbo].[BookingReservation]";

        private static string BOOKING_QUERY = "INSERT INTO BookingReservation (BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus)" +
                                                     "VALUES (@BookingReservationID, CAST(@BookingDate AS Date), @TotalPrice, @CustomerID, @BookingStatus)";

        private static string DETAIL_BOOKING_QUERY = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) " +
                                                     "VALUES (@BookingReservationID, @RoomID,  CAST(@StartDate AS Date),  CAST(@EndDate AS Date), @ActualPrice)";

        public static bool BookingRoom(BookingReservation booking, BookingDetail bookingDetails)
        {
            //SqlParameter[] parametersForBooking = new SqlParameter[] {
            //    new SqlParameter("@BookingReservationID", booking.BookingReservationId),
            //    new SqlParameter("@BookingDate", booking.BookingDate),
            //    new SqlParameter("@TotalPrice", booking.TotalPrice),
            //    new SqlParameter("@CustomerID", booking.CustomerId),
            //    new SqlParameter("@BookingStatus", booking.BookingStatus)
            //};

            //SqlParameter[] parametersForDetail = new SqlParameter[] {
            //    new SqlParameter("@BookingReservationID", booking.BookingReservationId),
            //    new SqlParameter("@RoomID", bookingDetails.RoomId),
            //    new SqlParameter("@StartDate", bookingDetails.StartDate),
            //    new SqlParameter("@EndDate", bookingDetails.EndDate),
            //    new SqlParameter("@ActualPrice", bookingDetails.ActualPrice)
            //};

            //DBContext.ExecuteNonQuerry(BOOKING_QUERY, parametersForBooking);
            //DBContext.ExecuteNonQuerry(DETAIL_BOOKING_QUERY, parametersForDetail);
            using var context = new FuminiHotelManagementContext();
            context.BookingReservations.Add(booking);
            context.BookingDetails.Add(bookingDetails);
            context.SaveChanges();
            return true;
        }

        public static List<BookingReservation> GetAllBooking()
        {
            using var context = new FuminiHotelManagementContext();
            List<BookingReservation> bookingList = (from bookings in context.BookingReservations
                                                   select bookings).ToList();
            return bookingList;
        }

        public static List<BookingDetail> GetBookingDetails(int bookingReservationId)
        {
            using var context = new FuminiHotelManagementContext();
            List<BookingDetail> bds = context.BookingDetails.Where(bd => bd.BookingReservationId == bookingReservationId).ToList();
            return bds;
        }
    }
}
