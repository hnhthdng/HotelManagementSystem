using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingReservationDAO : FuminiHotelManagementContext
    {
        //Using Singleton parttern
        private static BookingReservationDAO instance = null;
        private readonly static object instanceLock = new object();
        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<BookingReservation> GetAllBookingReservation()
        {
            var bookingReservation = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingReservation = context.BookingReservations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingReservation;
        }

        public BookingReservation GetBookingReservationByID(int id)
        {
            BookingReservation bookingReservation = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingReservation = context.BookingReservations.SingleOrDefault(c => c.BookingReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingReservation;

        }
        public IEnumerable<BookingReservation> SearchReservationByCustomerID(int customerID)
        {
            var bookingReservations = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingReservations = context.BookingReservations.Where(c => c.CustomerId == customerID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingReservations;
        }
        public void UpdateRoomInformation(BookingReservation bookingReservation)
        {
            BookingReservation _bookingReservation = GetBookingReservationByID(bookingReservation.BookingReservationId);
            try
            {
                if (_bookingReservation != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.BookingReservations.Update(bookingReservation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Reservation does not exist!");
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
