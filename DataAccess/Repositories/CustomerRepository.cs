using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerContext _customerContext;
        public CustomerRepository(ICustomerContext customerContext)
        {
            _customerContext = customerContext;
        }
        public async Task Create(Customer customer)
        {
            await _customerContext.Customers.InsertOneAsync(customer);
        }

        public async Task<bool> Delete(int id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(x => x.CustomerId, id);
            DeleteResult delete = await _customerContext.Customers.DeleteOneAsync(filter);
            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(x => x.CustomerId, id);
            return await _customerContext.Customers.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            //return await (await _customerContext.Customers.FindAsync(Builders<Customer>.Filter.Empty)).ToListAsync();
            var result = await _customerContext.Customers.Find(_ => true).ToListAsync();
            return result;
        }

        public async Task<bool> Update(Customer customer)
        {
            ReplaceOneResult update = await _customerContext.Customers.ReplaceOneAsync(filter: x => x.Id == customer.Id, replacement: customer);
            return update.IsAcknowledged && update.ModifiedCount > 0;
        }
    }

    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task Create(Customer customer);
        Task<bool> Update(Customer customer);
        Task<bool> Delete(int id);

    }
}
