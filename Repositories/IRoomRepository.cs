using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomRepository
    {
        void CreateNewRoom(RoomInformation room);
        List<RoomInformation> GetAllRoom();
        RoomInformation GetRoom(int roomId);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int roomId);
        RoomInformation GetRoomByRoomNumber(string roomNumber);
    }
}
