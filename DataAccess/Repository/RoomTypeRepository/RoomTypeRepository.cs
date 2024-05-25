using DataAccess.DAO;
using DataObject.Models;

namespace DataAccess.Repository.RoomTypeRepository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public IEnumerable<RoomType> GetAllRoomType() => RoomTypeDAO.Instance.GetAllRoomType();
        public RoomType GetRoomTypeByID(int id) => RoomTypeDAO.Instance.GetRoomTypeByID(id);
    }
}
