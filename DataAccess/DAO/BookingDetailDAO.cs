using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingDetailDAO : FuminiHotelManagementContext
    {
        //Using Singleton parttern
        private static BookingDetailDAO instance = null;
        private readonly static object instanceLock = new object();
        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId)
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetails = context.BookingDetails.Where(c => c.BookingReservationId == bookingReservationId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetails;
        }
    }
}
