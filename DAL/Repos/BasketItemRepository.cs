using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class BasketItemRepository : IBasketItemRepository
    {
        private readonly IConfiguration _configuration;

        public BasketItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(BasketItem entity)
        {
            var query = "INSERT INTO basket_item (basket_id, item_id) VALUES (@BasketId, @ItemId) RETURNING id";

            /*var parameters = new DynamicParameters();
            parameters.Add("CustomerName", entity.CustomerName);
            parameters.Add("PaysVAT", entity.PaysVAT);*/

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var newBasketId = await connection.QueryAsync<int>(query, entity);
                return newBasketId.Single();
            }
        }

        public Task<IEnumerable<BasketItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BasketItem> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(BasketItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
