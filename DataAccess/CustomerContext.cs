using DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess
{
    public class CustomerContext : ICustomerContext
    {
        private readonly IMongoDatabase _database;

        public CustomerContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
    }

    public interface ICustomerContext
    {
        IMongoCollection<Customer> Customers { get; }
    }
}
