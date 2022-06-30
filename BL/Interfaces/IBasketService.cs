using BL.ViewModels;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IBasketService
    {
        Task AddItem(int basketId, Item item);
        Task<BasketViewModel> GetBasketItems(int basketId);
        Task CloseBasket(int basketId);
    }
}
