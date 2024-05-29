using DataAccess.DAO;
using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.BookingDetailRepository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void CreateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.CreateBookingDetail(bookingDetail);

        public void DeleteBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.DeleteBookingDetail(bookingDetail);

        public void UpdateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.UpdateBookingDetail(bookingDetail);

        public IEnumerable<BookingDetail> GetALlBookingDetail() => BookingDetailDAO.Instance.GetALlBookingDetail();
        public IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId)
                => BookingDetailDAO.Instance.GetALlBookingDetailInReservation(bookingReservationId);

        public IEnumerable<BookingDetail> GetBookingDetailByRoomID(int id) => BookingDetailDAO.Instance.GetBookingDetailByRoomID(id);

        public IEnumerable<BookingDetail> GetBookingDetailsBetweenDates(DateOnly startDate, DateOnly endDate) => BookingDetailDAO.Instance.GetBookingDetailsBetweenDates(startDate, endDate);

    }
}
