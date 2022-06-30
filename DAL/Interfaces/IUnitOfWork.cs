

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IItemRepository Items { get; }
        IBasketRepository Baskets { get; }
    }
}
