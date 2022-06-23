using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using XSystem.Security.Cryptography;

namespace DiplomStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService db;
        private readonly UserManager<AppUser> userManager;
        private CartDTO cart;
        public OrderController(IOrderService _db, CartDTO cartDTO, UserManager<AppUser> _userManager)
        {
            db = _db;
            cart = cartDTO;
            userManager = _userManager;
        }
        public ViewResult Checkout() => View(new OrderViewModel());
        /// <summary>
        /// Проверяет форму заполнения данных
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            model.DateTime = DateTime.Now;
            //Если товаров в корзине нету, то выходит ошибка и сообщение
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }
            if (ModelState.IsValid)
            {
                PaidViewModel paid = new PaidViewModel();
                var user = userManager.Users.FirstOrDefault(p => p.UserName == User.Identity.Name);

                model.User = user;

                var mapCategor = new MapperConfiguration(c => c.CreateMap<OrderViewModel, Order>()).CreateMapper();
                //сопоставление объектов OrderViewModel в Order                                    
                var order = mapCategor.Map<OrderViewModel, Order>(model);
                //Присваиваем переменной товары из корзине
                order.Lines = cart.Lines.ToArray();
                db.SaveOrder(order);
                paid.Order = model;
                paid.Cart = cart;
                return View("Paid",paid);
            }
            return View(model);
        }            

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
