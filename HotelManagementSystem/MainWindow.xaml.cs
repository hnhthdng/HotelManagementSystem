using DataAccess.Repository.CustomerRepository;
using DataAccess.Repository.RoomInformationRepository;
using DataObject.Models;
using HotelManagementSystem.Admin.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        public MainWindow()
        {
            InitializeComponent();
            _roomInformationRepository = new RoomInformationRepository();
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
            }
            else
            {
                CustomerTab.Visibility = Visibility.Collapsed;
                ReservationTab.Visibility = Visibility.Collapsed;
                RoomTab.Visibility = Visibility.Collapsed;
                ReportTab.Visibility = Visibility.Collapsed;
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
            CreateAndUpdate createWindow = new CreateAndUpdate()
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
                CreateAndUpdate updateWindow = new CreateAndUpdate()
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
        #endregion

        #region Room Information Management Event Click
        private void refreshRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListViewRoomInformation();
        }

        private void createRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteRoomInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchByRoomNumberButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterByRoomTypeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}

