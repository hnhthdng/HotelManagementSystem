using DataAccess.Repository.BookingDetailRepository;
using DataAccess.Repository.BookingReservationRepository;
using DataAccess.Repository.RoomInformationRepository;
using DataObject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagementSystem.Admin.BookingDetailManagement
{
    /// <summary>
    /// Interaction logic for CreateAndUpdateBookingDetail.xaml
    /// </summary>
    public partial class CreateAndUpdateBookingDetail : Window
    {
        public bool isCreate { get; set; }
        public BookingDetail bookingDetail { get; set; }
        public IBookingDetailRepository _bookingDetailRepository;
        public IBookingReservationRepository _bookingReservationRepository;
        public MainWindow mainWindow { get; set; }
        public CreateAndUpdateBookingDetail()
        {
            InitializeComponent();
            _bookingReservationRepository = new BookingReservationRepository();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCreate)
            {
                this.Title = "Create Booking Detail";
                ActualPriceWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Title = "Update Booking Detail";
                BookingReservationIdTextBox.Text = bookingDetail.BookingReservationId.ToString();
                RoomIdTextBox.Text = bookingDetail.RoomId.ToString();
                StartDateDatePicker.Text = bookingDetail.StartDate.ToString();
                EndDateDatePicker.Text = bookingDetail.EndDate.ToString();
                ActualPriceTextBox.Text = bookingDetail.ActualPrice.ToString();

                BookingReservationIdWrapPanel.Visibility = Visibility.Collapsed;
                RoomIdWrapPanel.Visibility = Visibility.Collapsed;

            }
        }

        private void createOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            /*
             Create: 
                 Start Date < Booknig Date cua Reservation 
                Start Date < End Date
                Actual Price = price room * so ngay tu start to end

            Update 
                Start Date < Booknig Date cua Reservation 
                Start Date < End Date
             */

            try
            {
                DateTime? start = StartDateDatePicker.SelectedDate;
                DateTime? end = EndDateDatePicker.SelectedDate;

                BookingDetail booking = new BookingDetail
                {
                    BookingReservationId = Int32.Parse(BookingReservationIdTextBox.Text),
                    RoomId = Int32.Parse(RoomIdTextBox.Text),
                    StartDate = DateOnly.FromDateTime(start.Value),
                    EndDate = DateOnly.FromDateTime(end.Value),
                };

                //Get Booknig Reservation to check Start Date < Booknig Date cua Reservation 
                BookingReservation bookingReservation = _bookingReservationRepository.GetReservationByBookingDetatail(booking);
                if (!(CompareTwoDate(booking.StartDate, booking.EndDate) && CompareTwoDate(bookingReservation.BookingDate, booking.StartDate)))
                {
                    throw new Exception("Please enter valid Start Date");
                }


                if (isCreate)
                {
                    //Check if user change roomID, check this room is availble 
                    //if (!ÍsRoomAvailbleInBooking(booking)) throw new Exception("This room is in use !");

                    //Actual Price = price room* so ngay tu start to end

                    //_bookingReservationRepository.CreateBookingReservation(res);
                    //System.Windows.Forms.MessageBox.Show("Create Success !", "Create", MessageBoxButtons.OK);
                }
                else
                {
                    booking.ActualPrice = decimal.Parse(ActualPriceTextBox.Text);
                    _bookingDetailRepository.UpdateBookingDetail(booking);

                    
                    //Update total price in Booking Reservation
                    IEnumerable<BookingDetail> bookings = _bookingDetailRepository.GetALlBookingDetailInReservation(bookingReservation.BookingReservationId);
                    bookingReservation.TotalPrice = 0;
                    foreach (BookingDetail bookingDetail in bookings)
                    {

                        bookingReservation.TotalPrice += bookingDetail.ActualPrice;

                    }

                    _bookingReservationRepository.UpdateBookingReservation(bookingReservation);
                    _bookingDetailRepository.UpdateBookingDetail(booking);
                    System.Windows.Forms.MessageBox.Show("Update Success !", "Update", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                if (isCreate) System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CompareTwoDate(DateOnly? startDate, DateOnly endDate)
        {
            if (startDate <= endDate) return true;
            return false;
        }

        private bool ÍsRoomAvailbleInBooking(BookingDetail booking)
        {
            var bookingLists = _bookingDetailRepository.GetBookingDetailByRoomID(booking.RoomId);
            if (bookingLists.Any())
            {
                foreach (var bookingitem in bookingLists)
                {
                    if (bookingitem.EndDate < DateOnly.FromDateTime(DateTime.Now.Date)) return true;
                }
            }
            return true;
        }
    }
}
