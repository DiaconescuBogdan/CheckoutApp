using BL.Interfaces;
using BL.ViewModels;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BasketService : IBasketService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;

        public BasketService(IItemRepository itemRepository, IBasketRepository basketRepository, IBasketItemRepository basketItemRepository)
        {
            _itemRepository = itemRepository;
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
        }
        public async Task AddItem(int basketId, Item item)
        {
            var basket = await _basketRepository.GetById(basketId);
            if (basket != null)
            {
               var itemId = await _itemRepository.Add(item);
               var basketItemId = await _basketItemRepository.Add(new BasketItem()
                {
                    BasketId = basketId,
                    ItemId = itemId
                });
                
            }
        }

        public async Task<BasketViewModel> GetBasketItems(int basketId)
        {
            var vat = 10;
            double totalAmount = 0;
            var basket = (await _basketRepository.GetAllItems(basketId)).ToList().FirstOrDefault();
            basket?.Items.ForEach(item => totalAmount += item.Price);

            var test = decimal.Divide(vat, 100);
            var test2 = (decimal)totalAmount * test;
            var price = (decimal)totalAmount + test2;
            return new BasketViewModel()
            {
                Basket = basket,
                TotalAmountNet = totalAmount,
                TotalAmountGross = (double)((decimal)totalAmount + ((decimal)totalAmount * (decimal.Divide(vat, 100))))
            };
        }
        public async Task CloseBasket(int basketId)
        {
            await _basketRepository.CloseBasket(basketId);
        }

    }
}
