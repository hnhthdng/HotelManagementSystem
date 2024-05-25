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
        public IEnumerable<RoomInformation> GetRoomInformationByRoomNumber(string roomNumber)
        {
            var roomInformations = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomInformations = context.RoomInformations.Where(c => c.RoomNumber.Equals(roomNumber)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomInformations;

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
            IEnumerable<RoomInformation> _roomInformations = GetRoomInformationByRoomNumber(roomInformation.RoomNumber);
            try
            {
                RoomInformation _product = GetRoomInformationByRoomID(roomInformation.RoomId);
                if (_product != null)
                {
                    foreach (var room in _roomInformations)
                    {
                        if (room.RoomNumber == roomInformation.RoomNumber)
                        {
                            throw new Exception("The Room is already exist!");
                        }
                    }
                    if (roomInformation.RoomTypeId >= 1 && roomInformation.RoomTypeId <= 8)
                    {

                        using var context = new FuminiHotelManagementContext();
                        context.RoomInformations.Update(roomInformation);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The Room is has invalid room type id, please enter between 1 and 8!");
                    }

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
                RoomInformation _room = GetRoomInformationByRoomID(id);
                if (_room != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    _room.RoomStatus = 0;
                    context.RoomInformations.Update(_room);
                    context.SaveChanges();
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
            IEnumerable<RoomInformation> _roomInformations = GetRoomInformationByRoomNumber(roomInformation.RoomNumber);
            try
            {
                foreach(var room in _roomInformations)
                {
                    if(room.RoomNumber == roomInformation.RoomNumber)
                    {
                        throw new Exception("The Room is already exist!");
                    }
                }
                if (roomInformation.RoomTypeId >= 1 && roomInformation.RoomTypeId <= 8)
                {

                    using var context = new FuminiHotelManagementContext();
                    context.RoomInformations.Add(roomInformation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Room is has invalid room type id, please enter between 1 and 8!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
