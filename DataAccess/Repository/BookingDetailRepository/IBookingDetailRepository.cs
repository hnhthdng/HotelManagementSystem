using DataObject.Models;

namespace DataAccess.Repository.BookingDetailRepository
{
    public interface IBookingDetailRepository
    {
        IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId);
    }
}
