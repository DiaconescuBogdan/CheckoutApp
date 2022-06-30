using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class ItemRepository : IItemRepository
    {
        private readonly IConfiguration _configuration;

        public ItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(Item entity)
        {
            var query = "INSERT INTO item (Name, Price) VALUES (@Name, @Price) RETURNING id";
            
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var newItemId = await connection.QueryAsync<int>(query, entity);
                return newItemId.Single();
            }
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var query = "SELECT * FROM item";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Item>(query);
                return result.ToList();
            }
        }

        public async Task<Item> GetById(int id)
        {
            var query = "SELECT * FROM Item WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Item>(query, new { Id = id });
                return result;
            }
        }

        public Task<bool> Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Item entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
