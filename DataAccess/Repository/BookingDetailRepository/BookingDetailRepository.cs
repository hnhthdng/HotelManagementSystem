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
        public IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId)
                => BookingDetailDAO.Instance.GetALlBookingDetailInReservation(bookingReservationId);
    }
}
