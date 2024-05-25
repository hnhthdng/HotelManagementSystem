using DataObject.Models;

namespace DataAccess.DAO
{
    public class RoomTypeDAO : FuminiHotelManagementContext
    {
        //Using Singleton parttern
        private static RoomTypeDAO instance = null;
        private readonly static object instanceLock = new object();
        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }


        public IEnumerable<RoomType> GetAllRoomType()
        {
            var roomTypes = new List<RoomType>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomTypes = context.RoomTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomTypes;
        }
        public RoomType GetRoomTypeByID(int id)
        {
            RoomType roomType = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomType = context.RoomTypes.SingleOrDefault(c => c.RoomTypeId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomType;

        }
    }
}
