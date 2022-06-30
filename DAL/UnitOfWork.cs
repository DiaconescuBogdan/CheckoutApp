using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IItemRepository itemRepository, IBasketRepository basketRepository)
        {
            Items = itemRepository;
            Baskets = basketRepository;
        }
        public IItemRepository Items { get; }

        public IBasketRepository Baskets { get; }
    }
}
