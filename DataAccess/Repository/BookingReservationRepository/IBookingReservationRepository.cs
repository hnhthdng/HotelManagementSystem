using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.BookingReservationRepository
{
    public interface IBookingReservationRepository
    {
        BookingReservation GetBookingReservationByID(int id);
        IEnumerable<BookingReservation> SearchReservationByCustomerID(int customerID);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservationByID(int id);
        void CreateBookingReservation(BookingReservation bookingReservation);
        public IEnumerable<BookingReservation> GetAllBookingReservation();
    }
}
