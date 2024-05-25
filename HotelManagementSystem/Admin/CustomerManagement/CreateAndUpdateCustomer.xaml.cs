using DataAccess.Repository.CustomerRepository;
using DataObject.Models;
using System.Windows;
using System.Windows.Forms;

namespace HotelManagementSystem.Admin.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CreateAndUpdate.xaml
    /// </summary>
    public partial class CreateAndUpdateCustomer : Window
    {
        public bool isCreate {  get; set; }
        public Customer customer { get; set; }
        public ICustomerRepository _customerRepository;
        public MainWindow mainWindow {  get; set; }
        public CreateAndUpdateCustomer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCreate)
            {
                this.Title = "Create Customer";
                CustomerIDWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Title = "Update Customer";
                customerIDTextBox.Text = customer.CustomerId.ToString();
                customerFullNameTextBox.Text = customer.CustomerFullName;
                telephoneTextBox.Text = customer.Telephone;
                emailAddressTextBox.Text = customer.EmailAddress;
                customerBirthdayDatePicker.Text = customer.CustomerBirthday.ToString();
                customerStatusTextBox.Text = customer.CustomerStatus.ToString();
                passwordTextBox.Text = customer.Password;
            }
        }

        private void createOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer cus = new Customer
                {
                    CustomerFullName = customerFullNameTextBox.Text,
                    Telephone = telephoneTextBox.Text,
                    EmailAddress = emailAddressTextBox.Text,
                    CustomerBirthday = DateOnly.FromDateTime(customerBirthdayDatePicker.DisplayDate.Date),
                    CustomerStatus = byte.Parse(customerStatusTextBox.Text),
                    Password = passwordTextBox.Text,
                };
                if(isCreate)
                {
                    _customerRepository.Create(cus);
                    System.Windows.Forms.MessageBox.Show("Create Success !", "Create", MessageBoxButtons.OK);
                }
                else
                {
                    cus.CustomerId = customer.CustomerId;
                    _customerRepository.Update(cus);
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
