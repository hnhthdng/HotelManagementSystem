using DataObject.Models;

namespace DataAccess.DAO
{
    public class RoomInformationDAO :FuminiHotelManagementContext
    {
        //Using Singleton parttern
        private static RoomInformationDAO instance = null;
        private readonly static object instanceLock = new object();
        public static RoomInformationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<RoomInformation> GetAllRoomInformation()
        {
            var roomInformations = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInformations = context.RoomInformations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInformations;
        }
        public RoomInformation GetRoomInformationByRoomNumber(string roomNumber)
        {
            RoomInformation roomInformation = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInformation = context.RoomInformations.SingleOrDefault(c => c.RoomNumber.Equals(roomNumber));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInformation;

        }
        public RoomInformation GetRoomInformationByRoomID(int roomId)
        {
            RoomInformation roomInformation = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInformation = context.RoomInformations.SingleOrDefault(c => c.RoomId == roomId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInformation;

        }
        public IEnumerable<RoomInformation> FilterByRoomTypeID(int roomTypeId)
        {
            var roomInformations = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInformations = context.RoomInformations.Where(c => c.RoomTypeId == roomTypeId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInformations;
        }
        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                RoomInformation _product = GetRoomInformationByRoomID(roomInformation.RoomId);
                if (_product != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.RoomInformations.Update(roomInformation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The room does not already exist!");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteRoomInformationByID(int id)
        {
            try
            {
                RoomInformation _product = GetRoomInformationByRoomID(id);
                if (_product != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.RoomInformations.Remove(_product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Room does not already exist!");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreateRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                RoomInformation _roomInformation = GetRoomInformationByRoomNumber(roomInformation.RoomNumber);
                if (_roomInformation == null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.RoomInformations.Add(roomInformation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Room is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
