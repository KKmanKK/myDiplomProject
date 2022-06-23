using DiplomStore.BLL.DTO.Cart;
using DiplomStore.Domain.Entity;
using System.Linq;
using Xunit;
namespace DiplomStore.Tests
{

    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Организация - создание имитированного хранилища
            Tovars t1 = new Tovars { TovarsId = 1, name = "Микаса" };
            Tovars t2 = new Tovars { TovarsId = 2, name = "Наруто" };
            //Организация - создание корзины
            CartDTO cart = new CartDTO();
            //Действие
            cart.AddItem(t1, 1);
            cart.AddItem(t2, 1);
            CartLine[] result = cart.Lines.ToArray();
            //Утверждение - что товаров в корзине 2 штуки
            Assert.Equal(2, result.Length);
            //Утверждение - что товары из корзины равны товарам из временного хранилища
            Assert.Equal(t1.TovarsId, result[0].tovar.TovarsId);
            Assert.Equal(t2.TovarsId, result[1].tovar.TovarsId);
        }
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Организация - создание имитированного хранилища
            Tovars t1 = new Tovars { TovarsId = 1, name = "Микаса" };
            Tovars t2 = new Tovars { TovarsId = 2, name = "Наруто" };
            //Организация - создание корзины
            CartDTO cart = new CartDTO();
            //Действие
            cart.AddItem(t1, 1);
            cart.AddItem(t2, 3);
            cart.AddItem(t1, 4);
            CartLine[] result = cart.Lines.OrderBy(p=>p.tovar.TovarsId).ToArray();
            //Утверждение - что товаров в корзине 2 штуки
            Assert.Equal(2, result.Length);
            //Утверждение - что товаров определенное колличество
            Assert.Equal(3,result[1].Quantity);
            Assert.Equal(5,result[0].Quantity);
        }
        [Fact]
        public void Can_Remove_Line()
        {
            //Организация - создание имитированного хранилища
            Tovars t1 = new Tovars { TovarsId = 1, name = "Микаса" };
            Tovars t2 = new Tovars { TovarsId = 2, name = "Наруто" };
            //Организация - создание корзины
            CartDTO cart = new CartDTO();
            //Действие
            cart.AddItem(t1, 1);
            cart.AddItem(t2, 3);
            //Действие
            cart.RemoveLine(t1);
            //Утверждение - что нету товара в корзине
            Assert.Equal(0, cart.Lines.Where(p => p.tovar.TovarsId == t1.TovarsId).Count());
            Assert.Equal(1, cart.Lines.Count());
        }
        [Fact]
        public void Can_RemoveItem_of_Line()
        {
            //Организация - создание имитированного хранилища
            Tovars t1 = new Tovars { TovarsId = 1, name = "Микаса" };
            Tovars t2 = new Tovars { TovarsId = 2, name = "Наруто" };
            //Организация - создание корзины
            CartDTO cart = new CartDTO();
            //Действие
            cart.AddItem(t1, 1);
            cart.AddItem(t2, 3);
            //Действие
            cart.RemoveItem(t2);
            //Утверждение - что колличество товаров поменялось
            Assert.Equal(2,cart.Lines.FirstOrDefault(p=>p.tovar.TovarsId==t2.TovarsId).Quantity);            
        }
        [Fact]
        public void Can_Clear_Contents()
        {
            //Организация - создание имитированного хранилища
            Tovars t1 = new Tovars { TovarsId = 1, name = "Микаса" };
            Tovars t2 = new Tovars { TovarsId = 2, name = "Наруто" };
            //Организация - создание корзины
            CartDTO cart = new CartDTO();
            //Действие
            cart.AddItem(t1, 1);
            cart.AddItem(t2, 3);
            //Действие
            cart.Clear();
            //Утверждение - что нету товаров в корзине
            Assert.Equal(0, cart.Lines.Count());
        }

    }
}
