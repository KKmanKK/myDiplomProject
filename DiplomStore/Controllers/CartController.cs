using DiplomStore.BLL.DTO;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.ViewsModels.Cart;
using Microsoft.AspNetCore.Mvc;
using DiplomStore.BLL.DTO.Cart;

namespace DiplomStore.Controllers
{
    public class CartController:Controller
    {
        private readonly ITovarService Tovars;
        private readonly ITitleService Titles;
        private readonly ICategoryService Category;
        private readonly CartDTO cart;

        public CartController(ITovarService tv, ITitleService tit, ICategoryService cat, CartDTO car)
        {
            Tovars = tv;
            Titles = tit;
            Category = cat;
            cart = car;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public IActionResult AddToCart(int TovarsId, string returnUrl)
        {
            TovarDTO tovar = Tovars.GetTovar().FirstOrDefault(t=>t.TovarsId == TovarsId);
            if(tovar !=null)
            {
                cart.AddItem(tovar, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(int TovarsId, string returnUrl)
        {
            TovarDTO tovar = Tovars.Models().FirstOrDefault(t => t.TovarsId == TovarsId);
            if (tovar != null)
            {
                cart.RemoveLine(tovar);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public IActionResult RemoveItemFromCart(int TovarsId, string returnUrl)
        {
            TovarDTO tovar = Tovars.Models().FirstOrDefault(t => t.TovarsId == TovarsId);
            if (tovar != null)
            {
                cart.RemoveItem(tovar,1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

    }
}
