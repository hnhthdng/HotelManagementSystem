using DataAccess.Repository.CustomerRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HotelManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ICustomerRepository _customerRepository;
        public LoginWindow()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepository();
        }

        private bool isAdmin()
        {
            string _mail;
            string _password;
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            _mail = config["DefaultAccount:Email"];
            _password = config["DefaultAccount:Password"];

            if (string.Equals(_mail, emailTextBox.Text) && string.Equals(passwordBox.Password, _password)) return true;
            return false;
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordBox.Password;
            bool isAdmin = this.isAdmin();
            var customers = _customerRepository.GetCustomerList();
            var checkCustomer = _customerRepository.GetCustomerByEmail(email);

            if (isAdmin)
            {
                MainWindow main = new MainWindow()
                {
                    isAdmin = true,
                    _customerRepository = this._customerRepository
                };
                this.Hide();
                main.Closed += (s, args) => this.Show();
                main.Show();
            }
            else if (checkCustomer != null)
            {
                if (checkCustomer.CustomerStatus == 1)
                {
                    if (password == checkCustomer.Password)
                    {
                        MainWindow main = new MainWindow()
                        {
                            isAdmin = false,
                            customerInfo = checkCustomer,
                            _customerRepository = this._customerRepository
                        };
                        this.Hide();
                        main.Closed += (s, args) => this.Show();
                        main.Show();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Wrong password !", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("The account is deleted or do not present !", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Wrong password or email !", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
