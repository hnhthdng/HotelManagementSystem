using DataAccess.DAO;
using DataObject.Models;

namespace DataAccess.Repository.RoomInformationRepository
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public void CreateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.CreateRoomInformation(roomInformation);

        public void DeleteRoomInformationByID(int id) => RoomInformationDAO.Instance.DeleteRoomInformationByID(id);

        public IEnumerable<RoomInformation> FilterByRoomTypeID(int roomTypeId) => RoomInformationDAO.Instance.FilterByRoomTypeID(roomTypeId);

        public IEnumerable<RoomInformation> GetAllRoomInformation() => RoomInformationDAO.Instance.GetAllRoomInformation();

        public RoomInformation GetRoomInformationByRoomID(int roomID) => RoomInformationDAO.Instance.GetRoomInformationByRoomID(roomID);

        public IEnumerable<RoomInformation> GetRoomInformationByRoomNumber(string roomNumber) => RoomInformationDAO.Instance.GetRoomInformationByRoomNumber(roomNumber);

        public string GetRoomNumberFromBookingDetail(BookingDetail booking) => RoomInformationDAO.Instance.GetRoomNumberFromBookingDetail(booking);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);
    }
}
