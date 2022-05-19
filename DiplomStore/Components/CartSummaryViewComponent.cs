using DiplomStore.BLL.DTO.Cart;
using Microsoft.AspNetCore.Mvc;

namespace DiplomStore.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        private CartDTO Cart;
        public CartSummaryViewComponent(CartDTO cart)
        {
            Cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(Cart);
        }
    }
}
