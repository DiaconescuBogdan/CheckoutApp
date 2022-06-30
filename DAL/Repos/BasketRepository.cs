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
    public class BasketRepository : IBasketRepository
    {
        private readonly IConfiguration _configuration;

        public BasketRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(Basket entity)
        {
            var query = "INSERT INTO basket (customer_name, paysVAT) VALUES (@CustomerName, @PaysVAT) RETURNING id";

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

        public async Task CloseBasket(int basketId)
        {
            var sql = "UPDATE basket SET is_closed = true, is_payed = true WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { Id = basketId });
               // return true;
            }
        }

        public async Task<IEnumerable<Basket>> GetAll()
        {
            var query = "SELECT * FROM basket";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Basket>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Basket>> GetAllItems(int basketId)
        {
            var query = @"select b.*, i.id as itemId, i.name, i.price
                from basket b 
                inner join basket_item bi on b.id = bi.basket_id
                inner join item i on i.id = bi.item_id where b.id = @basketId";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var baskets = await connection.QueryAsync<Basket, Item, Basket>(query, (basket, item) =>
                {
                    basket.Items.Add(item);
                    return basket;
                }, new { basketId }, splitOn: "itemId");
                var result = baskets.GroupBy(b => b.Id).Select(g =>
                {
                    var groupedBasket = g.First();
                    groupedBasket.Items = g.Select(b => b.Items.Single()).ToList();
                    return groupedBasket;
                });

                return result;
            }
        }

        public async Task<Basket> GetById(int id)
        {
            var query = "SELECT * FROM basket WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Basket>(query, new { Id = id });
                return result;
            }
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Basket entity)
        {
            var sql = "UPDATE basket SET customer_name = @CustomerName, paysVAT = @PaysVAT WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, entity);
                return true;
            }
        }
    }
    
}
