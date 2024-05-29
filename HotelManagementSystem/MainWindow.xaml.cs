using DataAccess.Repository.BookingDetailRepository;
using DataAccess.Repository.BookingReservationRepository;
using DataAccess.Repository.CustomerRepository;
using DataAccess.Repository.RoomInformationRepository;
using DataAccess.Repository.RoomTypeRepository;
using DataObject.Models;
using HotelManagementSystem.Admin.CustomerManagement;
using HotelManagementSystem.Admin.ReservationManagement;
using HotelManagementSystem.Admin.RoomManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Customer customerInfo { get; set; }
        public bool isAdmin { get; set; }

        public ICustomerRepository _customerRepository;
        public IRoomInformationRepository _roomInformationRepository;
        public IRoomTypeRepository _roomTypeRepository;
        public IBookingReservationRepository _bookingReservationRepository;
        public IBookingDetailRepository _bookingDetailRepository;
        public MainWindow()
        {
            InitializeComponent();
            _roomInformationRepository = new RoomInformationRepository();
            _roomTypeRepository = new RoomTypeRepository();
            _bookingReservationRepository = new BookingReservationRepository();
            _bookingDetailRepository = new BookingDetailRepository();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstTabToHide.Visibility = Visibility.Collapsed;
            if (isAdmin)
            {
                BookingTab.Visibility = Visibility.Collapsed;
                ProfileTab.Visibility = Visibility.Collapsed;
                LoadListViewCustomer();
                LoadListViewRoomInformation();
                LoadListBoxRoomInformation();
                LoadListViewReservation();
                LoadListViewReport();
            }
            else
            {
                CustomerTab.Visibility = Visibility.Collapsed;
                ReservationTab.Visibility = Visibility.Collapsed;
                RoomTab.Visibility = Visibility.Collapsed;
                ReportTab.Visibility = Visibility.Collapsed;
                LoadProfileCustomer();
            }
        }

        #region Customer Management Method

        public void LoadListViewCustomer()
        {
            listCustomer.ItemsSource = null;
            listCustomer.ItemsSource = _customerRepository.GetCustomerList();
        }

        #endregion Customer Management Method

        #region Customer Management Event Click

        private void refreshCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListViewCustomer();
        }

        private void createCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAndUpdateCustomer createWindow = new CreateAndUpdateCustomer()
            {
                isCreate = true,
                _customerRepository = this._customerRepository,
                mainWindow = this
            };
            createWindow.Show();
            createWindow.Closed += (s, args) => this.LoadListViewCustomer();
        }

        private void updateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateAndUpdateCustomer updateWindow = new CreateAndUpdateCustomer()
                {
                    isCreate = false,
                    customer = _customerRepository.GetCustomerByID(Int32.Parse(customerIDTextBox.Text)),
                    _customerRepository = this._customerRepository,
                    mainWindow = this
                };
                updateWindow.Show();
                updateWindow.Closed += (s, args) => this.LoadListViewCustomer();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Please select customer before update !");
            }
        }

        private void deleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _customerRepository.Remove(Int32.Parse(customerIDTextBox.Text));
                    System.Windows.Forms.MessageBox.Show("Delete Success !");
                }
                LoadListViewCustomer();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void searchByNameCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchByNameTextBox.Text))
            {
                listCustomer.ItemsSource = null;
                listCustomer.ItemsSource = _customerRepository.GetCustomerByName(searchByNameTextBox.Text);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please enter before search !");
            }
        }

        #endregion Customer Management Event Click

        #region Room Information Management Method
        public void LoadListViewRoomInformation()
        {
            listRoomInformation.ItemsSource = null;
            listRoomInformation.ItemsSource = _roomInformationRepository.GetAllRoomInformation();
        }

        public void LoadListBoxRoomInformation()
        {
            var listRoomType = _roomTypeRepository.GetAllRoomType();
            foreach (RoomType r in listRoomType)
            {
                FilterByRoomTypeComboBox.Items.Add(r.RoomTypeId + ". " + r.RoomTypeName);
            }
        }
        public bool IsPassEndDate(IEnumerable<BookingDetail> bookingDetails)
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
        #endregion

        #region Room Information Management Event Click
        private void refreshRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListViewRoomInformation();
        }

        private void createRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAndUpdateRoom createWindow = new CreateAndUpdateRoom()
            {
                isCreate = true,
                _roomInformationRepository = this._roomInformationRepository,
                mainWindow = this
            };
            createWindow.Show();
            createWindow.Closed += (s, args) => this.LoadListViewRoomInformation();
        }

        private void updateRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateAndUpdateRoom updateWindow = new CreateAndUpdateRoom()
                {
                    isCreate = false,
                    room = _roomInformationRepository.GetRoomInformationByRoomID(Int32.Parse(RoomIdTextBox.Text)),
                    _roomInformationRepository = this._roomInformationRepository,
                    mainWindow = this
                };
                updateWindow.Show();
                updateWindow.Closed += (s, args) => this.LoadListViewRoomInformation();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Please select customer before update !");
            }
        }

        private void deleteRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

                var roomSelected = _roomInformationRepository.GetRoomInformationByRoomID(Int32.Parse(RoomIdTextBox.Text));
                IEnumerable<BookingDetail> booked = _bookingDetailRepository.GetBookingDetailByRoomID(roomSelected.RoomId);
                if (!booked.Any())
                {
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _roomInformationRepository.DeleteRoomInformationByID(Int32.Parse(RoomIdTextBox.Text));
                        System.Windows.Forms.MessageBox.Show("Delete Success !");
                    }
                    LoadListViewRoomInformation();
                }
                else
                {
                    if (!IsPassEndDate(booked))
                    {
                        System.Windows.Forms.MessageBox.Show("Delete Failed because this room is booked !");
                    }
                    else
                    {
                        _roomInformationRepository.DeleteRoomInformationByID(Int32.Parse(RoomIdTextBox.Text));
                        System.Windows.Forms.MessageBox.Show("Delete Success !");
                    }
                    LoadListViewRoomInformation();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void searchByRoomNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchByRoomNumberTextBox.Text))
            {
                listRoomInformation.ItemsSource = null;
                listRoomInformation.ItemsSource = _roomInformationRepository.GetRoomInformationByRoomNumber(searchByRoomNumberTextBox.Text);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please enter before search !");
            }
        }
        private void FilterByRoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoomType = FilterByRoomTypeComboBox.SelectedItem.ToString();
            var RoomTypeId = Int32.Parse(selectedRoomType.Substring(0, 1));
            var listRoomByRoomType = _roomInformationRepository.FilterByRoomTypeID(RoomTypeId);
            listRoomInformation.ItemsSource = null;
            listRoomInformation.ItemsSource = listRoomByRoomType;
        }


        #endregion

        #region Reservation Management Method
        public void LoadListViewReservation()
        {
            listBookingReservation.ItemsSource = null;
            listBookingReservation.ItemsSource = _bookingReservationRepository.GetAllBookingReservation();
        }
        #endregion

        #region Reservation Management Event Click
        private void refreshReservationButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListViewReservation();
        }

        private void createReservationButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAndUpdateReservation createWindow = new CreateAndUpdateReservation()
            {
                isCreate = true,
                _bookingReservationRepository = this._bookingReservationRepository,
                mainWindow = this
            };
            createWindow.Show();
            createWindow.Closed += (s, args) => this.LoadListViewReservation();
        }

        private void updateReservationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateAndUpdateReservation updateWindow = new CreateAndUpdateReservation()
                {
                    isCreate = false,
                    reservation = _bookingReservationRepository.GetBookingReservationByID(Int32.Parse(BookingReservationIdTextBox.Text)),
                    _bookingReservationRepository = this._bookingReservationRepository,
                    mainWindow = this
                };
                updateWindow.Show();
                updateWindow.Closed += (s, args) => this.LoadListViewReservation();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Please select reservation before update !");
            }
        }

        private void deleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _bookingReservationRepository.DeleteBookingReservationByID(Int32.Parse(BookingReservationIdTextBox.Text));
                    System.Windows.Forms.MessageBox.Show("Delete Success !");
                }
                LoadListViewReservation();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void searchReservationByCustomerIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchReservationByCustomerIDTextBox.Text))
            {
                int cusID = Int32.Parse(searchReservationByCustomerIDTextBox.Text);
                listBookingReservation.ItemsSource = null;
                listBookingReservation.ItemsSource = _bookingReservationRepository.SearchReservationByCustomerID(cusID);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please enter before search !");
            }
        }

        private void listBookingReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int reservationID = Int32.Parse(BookingReservationIdTextBox.Text);
                listBookingDetail.ItemsSource = null;
                listBookingDetail.ItemsSource = _bookingDetailRepository.GetALlBookingDetailInReservation(reservationID);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

            }
        }

        #endregion

        #region Report Method
        public void LoadListViewReport()
        {
            listReport.ItemsSource = null;
            listReport.ItemsSource = _bookingDetailRepository.GetALlBookingDetail();
        }
        #endregion

        #region Report Event Click

        private void RefreshDateButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListViewReport();
        }
       

        private void FilterDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? sdate = ReportStartDatePicker.SelectedDate;
                DateTime? edate = ReportEndDatePicker.SelectedDate;
                DateOnly startDate = DateOnly.FromDateTime(sdate.Value);
                DateOnly endDate = DateOnly.FromDateTime(edate.Value);
                listReport.ItemsSource = null;
                listReport.ItemsSource = _bookingDetailRepository.GetBookingDetailsBetweenDates(startDate, endDate);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Please enter start date and end date");
            }
        }


        #endregion

        #region Profile Method
        public void LoadProfileCustomer()
        {
            if(customerInfo != null)
            {
                customerProfileIDTextBox.Text = customerInfo.CustomerId.ToString();
                customerFullNameProfileTextBox.Text = customerInfo.CustomerFullName;
                telephoneProfileTextBox.Text = customerInfo.Telephone;
                emailAddressProfileTextBox.Text = customerInfo.EmailAddress;
                customerBirthdayProfileDatePicker.Text = customerInfo.CustomerBirthday.ToString();
                customerStatusProfileTextBox.Text = customerInfo.CustomerStatus.ToString();
                passwordProfileTextBox.Text = customerInfo.Password;
            }
        }
        #endregion

        #region Profile Event Click
        private void refreshCustomerProfileButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProfileCustomer();
        }

        private void updateCustomerProfileButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? birthday = customerBirthdayProfileDatePicker.SelectedDate;

            Customer cus = new Customer
            {
                CustomerId = customerInfo.CustomerId,
                CustomerFullName = customerFullNameProfileTextBox.Text,
                Telephone = telephoneProfileTextBox.Text,
                EmailAddress = emailAddressProfileTextBox.Text,
                CustomerBirthday = DateOnly.FromDateTime(birthday.Value),
                CustomerStatus = byte.Parse(customerStatusProfileTextBox.Text),
                Password = passwordProfileTextBox.Text,
            };

            _customerRepository.Update(cus);
            customerInfo = cus;
            System.Windows.Forms.MessageBox.Show("Update Success !", "Update", MessageBoxButtons.OK);
        }
        #endregion

    }
}

