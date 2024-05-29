using DataAccess.DAO;
using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.BookingReservationRepository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void CreateBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.Instance.CreateBookingReservation(bookingReservation);

        public void DeleteBookingReservationByID(int id) => BookingReservationDAO.Instance.DeleteBookingReservationByID(id);

        public IEnumerable<BookingReservation> GetAllBookingReservation() => BookingReservationDAO.Instance.GetAllBookingReservation();

        public BookingReservation GetBookingReservationByID(int id) => BookingReservationDAO.Instance.GetBookingReservationByID(id);

        public BookingReservation GetReservationByBookingDetatail(BookingDetail bookingDetail) => BookingReservationDAO.Instance.GetReservationByBookingDetatail(bookingDetail);

        public IEnumerable<BookingReservation> SearchReservationByCustomerID(int customerID) => BookingReservationDAO.Instance.SearchReservationByCustomerID((int)customerID);

        public void UpdateBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.Instance.UpdateBookingReservation(bookingReservation);
    }
}
