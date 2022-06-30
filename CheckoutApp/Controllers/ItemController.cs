

using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<Item> GetById(int id)
        {
            var product = await _unitOfWork.Items.GetById(id);
            return product;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            var products = await _unitOfWork.Items.GetAll();
            return products;
        }


        [HttpPost]
        public async Task<int> Create(Item item)
        {
            return await _unitOfWork.Items.Add(item);
        }
    }
}
