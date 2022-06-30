using BL.Interfaces;
using BL.ViewModels;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public BasketController(IUnitOfWork unitOfWork, IBasketService basketService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<Item> GetById(int id)
        {
            var product = await _unitOfWork.Items.GetById(id);
            return product;
        }

        [HttpGet]
        public async Task<IEnumerable<Basket>> GetAll()
        {
            var baskets = await _unitOfWork.Baskets.GetAll();
            return baskets;
        }


        [HttpPost]
        public async Task<int> Create(Basket basket)
        {
            return await _unitOfWork.Baskets.Add(basket);
        }

        [HttpPut]
        public async Task<bool> Edit(Basket basket)
        {
            var p = await _unitOfWork.Baskets.GetById(basket.Id);
            if (p == null)
                return false;
            bool update = await _unitOfWork.Baskets.Update(basket);

            return update;
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task AddItem(int basketId, Item item)
        {
            await _basketService.AddItem(basketId, item);
        }

        [HttpGet]
        [Route("GetBasketItems")]
        public async Task<BasketViewModel> GetBasketItems(int basketId)
        {
            return await _basketService.GetBasketItems(basketId);
        }

        [HttpPatch("{basketId}")]
        public async Task CloseBasket(int basketId)
        {
            await _basketService.CloseBasket(basketId);
        }
    }
}
