using BusinessObject;
using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls.Primitive;

namespace DataAccessLayer
{
    public class RoomDAO
    {
        public static List<RoomInformation> GetAllRoom()
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomInformations.ToList();
        }

        public static void CreateNewRoom(RoomInformation room)
        {   
            using var context = new FuminiHotelManagementContext();
            context.RoomInformations.Add(room);
            int rows = context.SaveChanges();
            Console.WriteLine($"Added {rows} room");
        }

        public static void UpdateRoom(RoomInformation room)
        {
            using var context = new FuminiHotelManagementContext();
            RoomInformation roomInformation = (from r in context.RoomInformations
                                   where r.RoomId == room.RoomId
                                   select r).FirstOrDefault();
            if(roomInformation != null)
            {
                roomInformation.RoomNumber = room.RoomNumber;
                roomInformation.RoomDetailDescription = room.RoomDetailDescription;
                roomInformation.RoomMaxCapacity = room.RoomMaxCapacity;
                roomInformation.RoomStatus = room.RoomStatus;
                roomInformation.RoomPricePerDay = room.RoomPricePerDay;
                roomInformation.RoomTypeId = room.RoomTypeId;
                roomInformation.RoomId = room.RoomId;

                int rows = context.SaveChanges();
                Console.WriteLine($"Updated {rows} room");
            }
        }

        public static void DeleteRoom(int RoomId)
        {
            using var context = new FuminiHotelManagementContext();
            RoomInformation room = (from r in context.RoomInformations
                                    where r.RoomId == RoomId
                                    select r).FirstOrDefault();
            context.RoomInformations.Remove(room);
            int rows = context.SaveChanges();
            Console.WriteLine($"Deleted {rows} room");
        }

        public static RoomInformation GetRoom(int RoomId) 
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.RoomInformations.FirstOrDefault(r => r.RoomId == RoomId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static RoomInformation GetRoomByRoomNumber(string roomNumber)
        {
                using var context = new FuminiHotelManagementContext();
                return context.RoomInformations.FirstOrDefault(r => r.RoomNumber.Equals(roomNumber));
        }
    }
}
