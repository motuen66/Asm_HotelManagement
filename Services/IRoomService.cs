﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoomService
    {
        void CreateNewRoom(RoomInformation room);
        List<RoomInformation> GetAllRoom();
        RoomInformation GetRoom(int roomId);
        void UpdateRoom(RoomInformation room); 
        void DeleteRoom(int roomId);
        RoomInformation GetRoomByRoomNumber(string RoomNumber);
    }
}
