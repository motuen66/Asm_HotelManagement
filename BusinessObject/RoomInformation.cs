using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class RoomInformation
{
    public RoomInformation()
    {
    }
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? RoomDetailDescription { get; set; }

    public int? RoomMaxCapacity { get; set; }

    public int RoomTypeId { get; set; }

    public byte? RoomStatus { get; set; }

    public decimal? RoomPricePerDay { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;

    public RoomInformation(int roomId, string roomNumber, string? roomDetailDescription, int? roomMaxCapacity, int roomTypeId, byte? roomStatus, decimal? roomPricePerDay, ICollection<BookingDetail> bookingDetails, RoomType roomType)
    {
        RoomId = roomId;
        RoomNumber = roomNumber;
        RoomDetailDescription = roomDetailDescription;
        RoomMaxCapacity = roomMaxCapacity;
        RoomTypeId = roomTypeId;
        RoomStatus = roomStatus;
        RoomPricePerDay = roomPricePerDay;
        BookingDetails = bookingDetails;
        RoomType = roomType;
    }

    public RoomInformation(int roomID, string roomNumber, string roomDescription, int roomMaxCapacity, byte roomStatus, decimal? roomPricePerDate, int roomTypeID)
    {
        RoomId = roomID;
        RoomNumber = roomNumber;
        RoomDetailDescription = roomDescription;
        RoomMaxCapacity = roomMaxCapacity;
        RoomStatus = roomStatus;
        RoomPricePerDay = roomPricePerDate;
        RoomTypeId = roomTypeID;
    }

    public RoomInformation(int roomID, string? roomNumber, string? roomDescription, int roomMaxCapacity, byte roomStatus, decimal roomPricePerDate, string? roomTypeName)
    {
        RoomId = roomID;
        RoomNumber = roomNumber;
        RoomMaxCapacity = roomMaxCapacity;
        RoomStatus = roomStatus;
    }
}
