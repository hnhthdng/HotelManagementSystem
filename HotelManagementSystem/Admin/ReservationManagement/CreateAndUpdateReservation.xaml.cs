using DataAccess.Repository.BookingReservationRepository;
using DataObject.Models;
using System.Windows;
using System.Windows.Forms;

namespace HotelManagementSystem.Admin.ReservationManagement
{
    /// <summary>
    /// Interaction logic for CreateAndUpdateReservation.xaml
    /// </summary>

    public partial class CreateAndUpdateReservation : Window
    {
        public bool isCreate { get; set; }
        public BookingReservation reservation { get; set; }
        public IBookingReservationRepository _bookingReservationRepository;
        public MainWindow mainWindow { get; set; }

        public CreateAndUpdateReservation()
        {
            InitializeComponent();
        }

        private void createOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BookingReservation res = new BookingReservation
                {
                    BookingDate = DateOnly.FromDateTime(BooingDatePicker.DisplayDate.Date),
                    TotalPrice = decimal.Parse(totalPriceTextBox.Text),
                    CustomerId = Int32.Parse(customerIDTextBox.Text),
                    BookingStatus = byte.Parse(reservationStatusTextBox.Text),
                };
                if (isCreate)
                {
                    _bookingReservationRepository.CreateBookingReservation(res);
                    System.Windows.Forms.MessageBox.Show("Create Success !", "Create", MessageBoxButtons.OK);
                }
                else
                {
                    res.BookingReservationId = reservation.BookingReservationId;
                    _bookingReservationRepository.UpdateBookingReservation(res);
                    System.Windows.Forms.MessageBox.Show("Update Success !", "Update", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                if (isCreate) System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCreate)
            {
                this.Title = "Create Reservation";
                ReservationIDWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Title = "Update Reservation";
                reservationIDTextBox.Text = reservation.BookingReservationId.ToString();
                BooingDatePicker.Text = reservation.BookingDate.ToString();
                totalPriceTextBox.Text = reservation.TotalPrice.ToString();
                customerIDTextBox.Text = reservation.CustomerId.ToString();
                reservationStatusTextBox.Text = reservation.BookingStatus.ToString();
            }
        }
    }
}
