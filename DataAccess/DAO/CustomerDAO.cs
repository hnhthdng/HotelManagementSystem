using DataObject.Models;

namespace DataAccess.DAO
{
    public class CustomerDAO : FuminiHotelManagementContext
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Customer> GetCustomerList()
        {
            var customers = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                customers = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public Customer GetCustomerByID(int customerID)
        {
            Customer customer = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                customer = context.Customers.SingleOrDefault(c => c.CustomerId == customerID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            Customer customer = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                customer = context.Customers.SingleOrDefault(c => c.EmailAddress == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public IEnumerable<Customer> GetCustomerByName(string name)
        {
            var customers = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                customers = context.Customers.Where(n => n.CustomerFullName.Contains(name)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public void Create(Customer customer)
        {
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer == null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The customer is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    context.Customers.Update(customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The customer does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int customerID)
        {
            try
            {
                Customer _customer = GetCustomerByID(customerID);
                if (_customer != null)
                {
                    using var context = new FuminiHotelManagementContext();
                    //context.Customers.Remove(_customer);
                    //context.SaveChanges();
                    _customer.CustomerStatus = 0;
                    context.Customers.Update(_customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The customer does not already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}