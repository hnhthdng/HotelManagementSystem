using DataObject.Models;

namespace DataAccess.Repository.CustomerRepository
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetCustomerList();
        public Customer GetCustomerByID(int customerID);
        public Customer GetCustomerByEmail(string email);
        public IEnumerable<Customer> GetCustomerByName(string name);
        public void Create(Customer customer);
        public void Update(Customer customer);
        public void Remove(int customerID);
    }
}
