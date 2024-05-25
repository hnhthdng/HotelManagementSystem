using DataAccess.DAO;
using DataObject.Models;

namespace DataAccess.Repository.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {

        public Customer GetCustomerByID(int customerID) => CustomerDAO.Instance.GetCustomerByID(customerID);
        public Customer GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);
        public IEnumerable<Customer> GetCustomerList() => CustomerDAO.Instance.GetCustomerList();
        public void Remove(int customerID) => CustomerDAO.Instance.Remove(customerID);
        public void Update(Customer customer) => CustomerDAO.Instance.Update(customer);
        public void Create(Customer customer) => CustomerDAO.Instance.Create(customer);
        public IEnumerable<Customer> GetCustomerByName(string name) => CustomerDAO.Instance.GetCustomerByName(name);
    }
}
