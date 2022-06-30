using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<IEnumerable<Basket>> GetAllItems(int basketId);
        Task CloseBasket(int basketId);
    }
}
