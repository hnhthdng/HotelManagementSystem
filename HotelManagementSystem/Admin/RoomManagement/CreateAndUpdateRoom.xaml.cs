using DataAccess.Repository.BookingDetailRepository;
using DataAccess.Repository.RoomInformationRepository;
using DataObject.Models;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace HotelManagementSystem.Admin.RoomManagement
{
    /// <summary>
    /// Interaction logic for CreateAndUpdate.xaml
    /// </summary>
    public partial class CreateAndUpdateRoom : Window
    {
        public bool isCreate { get; set; }
        public RoomInformation room { get; set; }
        public IRoomInformationRepository _roomInformationRepository;
        public IBookingDetailRepository _bookingDetailRepository;
        public MainWindow mainWindow { get; set; }

        public CreateAndUpdateRoom()
        {
            InitializeComponent();
            _bookingDetailRepository = new BookingDetailRepository();
        }


        public bool IsPassEndDate(IEnumerable< BookingDetail> bookingDetails)
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            foreach (var book in bookingDetails)
            {
                if (book.EndDate >= dateNow)
                {
                    return false;
                }
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCreate)
            {
                this.Title = "Create Room";
                RoomIDWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Title = "Update Room";
                roomIDTextBox.Text = room.RoomId.ToString();
                roomNumberTextBox.Text = room.RoomNumber;
                roomDetailDescriptionTextBox.Text = room.RoomDetailDescription;
                RoomMaxCapacityTextBox.Text = room.RoomMaxCapacity.ToString();
                RoomTypeIdTextBox.Text = room.RoomTypeId.ToString();
                roomStatusTextBox.Text = room.RoomStatus.ToString();
                RoomPricePerDayTextBox.Text = room.RoomPricePerDay.ToString();


                IEnumerable<BookingDetail> booked = _bookingDetailRepository.GetBookingDetailByRoomID(room.RoomId);
                if (!IsPassEndDate(booked)) {

                    RoomNumberPanel.Visibility = Visibility.Collapsed;
                    RoomDetailWrapPanel.Visibility = Visibility.Collapsed;
                    RoomMaxCapacityPanel.Visibility = Visibility.Collapsed;
                    RoomTypeIDWrapPanel.Visibility = Visibility.Collapsed;
                    RoomPriceWrapPanel.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void createOrUpdateRoomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomInformation _room = new RoomInformation
                {
                    RoomNumber = roomNumberTextBox.Text,
                    RoomDetailDescription = roomDetailDescriptionTextBox.Text,
                    RoomMaxCapacity = Int32.Parse(RoomMaxCapacityTextBox.Text),
                    RoomTypeId = Int32.Parse(RoomTypeIdTextBox.Text),
                    RoomStatus = byte.Parse(roomStatusTextBox.Text),
                    RoomPricePerDay = decimal.Parse(RoomPricePerDayTextBox.Text),
                };
                if (isCreate)
                {
                    _roomInformationRepository.CreateRoomInformation(_room);
                    System.Windows.Forms.MessageBox.Show("Create Success !", "Create", MessageBoxButtons.OK);
                }
                else
                {
                    
                    _room.RoomId = room.RoomId;
                    _roomInformationRepository.UpdateRoomInformation(_room);
                    System.Windows.Forms.MessageBox.Show("Update Success !", "Update", MessageBoxButtons.OK);

                }
            }
            catch (Exception ex)
            {
                if (isCreate) System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else System.Windows.Forms.MessageBox.Show(ex.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
