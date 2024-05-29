using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RoomInformationRepository
{
    public interface IRoomInformationRepository
    {
        IEnumerable<RoomInformation> GetAllRoomInformation();
        IEnumerable<RoomInformation> GetRoomInformationByRoomNumber(string roomNumber);
        RoomInformation GetRoomInformationByRoomID(int roomID);
        IEnumerable<RoomInformation> FilterByRoomTypeID(int roomTypeId);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformationByID(int id);
        void CreateRoomInformation(RoomInformation roomInformation);
        public string GetRoomNumberFromBookingDetail(BookingDetail booking);

    }
}
