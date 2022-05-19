using DiplomStore.BLL.DTO.Cart;

namespace DiplomStore.ViewsModels.Cart
{
    public class CartIndexViewModel
    {
        public CartDTO Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
