using DataObject.Models;
using Microsoft.EntityFrameworkCore;
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
        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
           
            try
            {
                if (bookingReservation != null)
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
        public void DeleteBookingReservationByID(int id)
        {
            try
            {
                BookingReservation _bookingReservation = GetBookingReservationByID(id);
                if (_bookingReservation != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    _bookingReservation.BookingStatus = 0;
                    context.BookingReservations.Update(_bookingReservation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Reservation does not already exist!");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreateBookingReservation(BookingReservation bookingReservation)
        {
            BookingReservation _bookingReservations = GetBookingReservationByID(bookingReservation.BookingReservationId);
            try
            {
                if (_bookingReservations == null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.BookingReservations.Add(bookingReservation);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The reservation is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookingReservation GetReservationByBookingDetatail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var reservation = context.BookingReservations.Include(b => b.BookingDetails).FirstOrDefault(c => c.BookingReservationId == bookingDetail.BookingReservationId);
                return reservation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
