using DiplomStore.BLL.DTO.Cart;

namespace DiplomStore.ViewsModels.Order
{
    public class PaidViewModel
    {
        public CartDTO Cart { get; set; }
        public OrderViewModel Order { get; set; }

    }
}
