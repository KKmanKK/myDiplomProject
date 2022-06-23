using AutoMapper;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Order;
using DiplomStore.ViewsModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DiplomStore.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IOrderService db;
        private readonly UserManager<AppUser> userManager;
        public UserController(IOrderService orderService, UserManager<AppUser> _userManager)
        {
            db = orderService;
            userManager = _userManager;
        }
        public ViewResult Index()
        {
            UserViewModel userOrd = new UserViewModel();
            var user = userManager.Users.FirstOrDefault(p => p.UserName == User.Identity.Name);
            var map = new MapperConfiguration(c => c.CreateMap<Order, OrderViewModel>()).CreateMapper();
            //сопоставление объектов Order в OrderViewModel                    
            var order = map.Map<IEnumerable<Order>, List<OrderViewModel>>(db.Orders().Where(p => p.User.Id == user.Id));
            userOrd.orders = order;

            return View(userOrd);
        }
        public IActionResult DeleteOrder(int IdOrder)
        {
            if (IdOrder != 0)
            {
                db.Delete(IdOrder);
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
