using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Controllers;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using Xunit;
namespace DiplomStore.Tests
{
    public class OrderControllerTests
    {
        private readonly UserManager<AppUser> userManager;

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderService> mock = new Mock<IOrderService>();            

            CartDTO cart = new CartDTO();

            Order ord = new Order();
            var map = new MapperConfiguration(c => c.CreateMap<Order, OrderViewModel>()).CreateMapper();
            var order = map.Map<Order, OrderViewModel>(ord);

            OrderController orderController = new OrderController(mock.Object ,cart, userManager);

            ViewResult result = orderController.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid()
        {
            Mock<IOrderService> mock = new Mock<IOrderService>();

            CartDTO cart = new CartDTO();
            cart.AddItem(new Tovars { TovarsId = 1, name = "Микаса" },1);        

            OrderController orderController = new OrderController(mock.Object, cart, userManager);
            orderController.ModelState.AddModelError("error", "error");

            ViewResult result = orderController.Checkout(new OrderViewModel()) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }       
    }
}
