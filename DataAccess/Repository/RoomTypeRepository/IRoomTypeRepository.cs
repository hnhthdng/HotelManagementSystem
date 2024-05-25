using DataObject.Models;

namespace DataAccess.Repository.RoomTypeRepository
{
    public interface IRoomTypeRepository
    {
        IEnumerable<RoomType> GetAllRoomType();
        RoomType GetRoomTypeByID(int id);
    }
}
