using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Controllers;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiplomStore.Tests
{
    public class AdminControllerTests
    {
        private readonly UserManager<AppUser> userManager;
        [Fact]
        public void Index_All_Tovars()
        {
            //Организация - создание имитированного хранилища
            Mock<ITovarService> mockTovar = new Mock<ITovarService>();
            Mock<ITitleService> mockTitle = new Mock<ITitleService>();
            Mock<ICategoryService> mockCategory = new Mock<ICategoryService>();
            Mock<IOrderService> mockOrder = new Mock<IOrderService>();

            mockTovar.Setup(m => m.Models()).Returns(new Tovars[]
            {
                new Tovars {TovarsId = 1, name="Микаса"},
                new Tovars { TovarsId = 2, name= "Леви"}
            });
            //Организация - создание контроллера
            AdminController adminController = new AdminController(mockTovar.Object, mockTitle.Object, 
                mockCategory.Object, mockOrder.Object, userManager);
            //Действие
            TovarViewModel[] result = GetViewMode<IEnumerable<TovarViewModel>>(adminController.IndexTovars())?.ToArray();
            //Утверждение - что товаров 2 штуки
            Assert.Equal(2, result.Length);
            //Утверждение - что товар называется Микаса
            Assert.Equal("Микаса", result[0].name);
            //Утверждение - что товар называется Леви
            Assert.Equal("Леви", result[1].name);
        }

        [Fact]
        public void Can_Delete_Tovar()
        {
            //Организация - создание имитированного хранилища
            Mock<ITovarService> mockTovar = new Mock<ITovarService>();
            Mock<ITitleService> mockTitle = new Mock<ITitleService>();
            Mock<ICategoryService> mockCategory = new Mock<ICategoryService>();
            Mock<IOrderService> mockOrder = new Mock<IOrderService>();

            Tovars tovar = new Tovars { TovarsId = 2, name = "Леви" };

            mockTovar.Setup(m => m.Models()).Returns(new Tovars[]
           {
                new Tovars {TovarsId = 1, name="Микаса"},
                tovar,
                new Tovars {TovarsId= 3 , name="Эрен"}
           });
            //Организация - создание контроллера
            AdminController adminController = new AdminController(mockTovar.Object, mockTitle.Object,
                mockCategory.Object, mockOrder.Object, userManager);
            //Действие
            adminController.DeleteTovar(tovar.TovarsId);
            //Утверждение - что удаление товара прошло успешно
            mockTovar.Verify(m => m.Delete(tovar.TovarsId));

        }

        [Fact]
        public void Can_Create_Tovar()
        {
            //Организация - создание имитированного хранилища
            Mock<ITovarService> mockTovar = new Mock<ITovarService>();
            Mock<ITitleService> mockTitle = new Mock<ITitleService>();
            Mock<ICategoryService> mockCategory = new Mock<ICategoryService>();
            Mock<IOrderService> mockOrder = new Mock<IOrderService>();

            mockCategory.Setup(m => m.Models()).Returns(new Categories[]
           {
                new Categories {CategoriesId = 1, name="Кружка"},
                new Categories {CategoriesId= 2 , name="Сумка"}
           });

            mockTitle.Setup(m => m.Models()).Returns(new Titles[]
          {
                new Titles {TitlesId = 1, name="Атака титанов"},
                new Titles {TitlesId= 2 , name="Наруто"}
          });

            mockTovar.Setup(m => m.Models()).Returns(new Tovars[]
           {
                new Tovars {TovarsId = 1, name="Микаса"},               
                new Tovars {TovarsId= 3 , name="Эрен"}
           });
             //Организация - создание контроллера
            AdminController adminController = new AdminController(mockTovar.Object, mockTitle.Object,
                mockCategory.Object, mockOrder.Object, userManager);
                                   
            var categories = mockCategory.Object.Models().FirstOrDefault(p => p.name == "Кружка");
                                  
            var titles = mockTitle.Object.Models().FirstOrDefault(p => p.name == "Атака титанов");


            //Действие
            TovarViewModel tovarViewModel = new TovarViewModel { TovarsId = 2, name="Леви", category= categories, titles= titles, count=12, price=750, isValids=true };
                                    
            var tovar = tovarViewModel;


            IActionResult result = adminController.CreateTovar(tovarViewModel);

            //Утверждение - что тип IActionResult является RedirectToActionResult
            Assert.IsType<RedirectToActionResult>(result);
            //Утверждение - что имя метода называеся IndexTovars
            Assert.Equal("IndexTovars", (result as RedirectToActionResult).ActionName);

        }

        private T GetViewMode<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
        
    }
}
