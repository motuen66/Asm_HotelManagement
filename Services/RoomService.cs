﻿using BusinessObject;
using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository iRoomRepository = new RoomRepository();
        public void CreateNewRoom(RoomInformation room)
        {
            if (iRoomRepository.GetRoomByRoomNumber(room.RoomNumber) != null)
            {
                throw new Exception("Room number already exist.");
            }
            else
            {
                iRoomRepository.CreateNewRoom(room);
            }
        }

        public void DeleteRoom(int roomId)
        {
            iRoomRepository.DeleteRoom(roomId);
        }

        public List<RoomInformation> GetAllRoom()
        {
            return iRoomRepository.GetAllRoom();
        }

        public RoomInformation GetRoom(int roomId)
        {
            return iRoomRepository.GetRoom(roomId);
        }

        public void UpdateRoom(RoomInformation room)
        {
            iRoomRepository.UpdateRoom(room);
        }

        public RoomInformation GetRoomByRoomNumber(string roomNumber)
        {
            return iRoomRepository.GetRoomByRoomNumber(roomNumber);
        }

    }
}
