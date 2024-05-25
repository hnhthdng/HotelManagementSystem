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

        public RoomInformation GetRoomInformationByRoomNumber(string roomNumber) => RoomInformationDAO.Instance.GetRoomInformationByRoomNumber(roomNumber);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);
    }
}
