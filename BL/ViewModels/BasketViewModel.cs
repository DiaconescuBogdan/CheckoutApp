using DAL.Models;

namespace BL.ViewModels
{
    public class BasketViewModel
    {
        public Basket Basket { get; set; }
        public double TotalAmountNet { get; set; }
        public double TotalAmountGross { get; set; }
    }
}
