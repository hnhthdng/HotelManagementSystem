using DataObject.Models;

namespace DataAccess.Repository.BookingDetailRepository
{
    public interface IBookingDetailRepository
    {
        IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId);
        IEnumerable<BookingDetail> GetBookingDetailByRoomID(int id);
        IEnumerable<BookingDetail> GetALlBookingDetail();
        IEnumerable<BookingDetail> GetBookingDetailsBetweenDates(DateOnly startDate, DateOnly endDate);
        public void CreateBookingDetail(BookingDetail bookingDetail);
        public void UpdateBookingDetail(BookingDetail bookingDetail);
        public void DeleteBookingDetail(BookingDetail bookingDetail);
    }
}
