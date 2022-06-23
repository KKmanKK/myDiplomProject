using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Controllers;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels;
using DiplomStore.ViewsModels.Title;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiplomStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Tovars_Title_Category_Index()
        {   
            //Организация - создание имитированного хранилища
            Mock<ITovarService> mockTovar = new Mock<ITovarService>();
            Mock<ITitleService> mockTitle = new Mock<ITitleService>();
            Mock<ICategoryService> mockCategory = new Mock<ICategoryService>();

            mockCategory.Setup(m => m.Models()).Returns(new Categories[]
          {
                new Categories {CategoriesId = 1, name="Кружка" , IsPopular=true},
                new Categories {CategoriesId= 2 , name="Сумка" , IsPopular=true}
          });

            mockTitle.Setup(m => m.Models()).Returns(new Titles[]
          {
                new Titles {TitlesId = 1, name="Атака титанов" , isPopular=true},
                new Titles {TitlesId= 2 , name="Наруто" , isPopular=true}
          });

            mockTovar.Setup(m => m.Models()).Returns(new Tovars[]
           {
                new Tovars {TovarsId = 1, name="Микаса" , isValids=true},
                new Tovars {TovarsId= 3 , name="Эрен" , isValids=true}
           });

            //Организация - создание контроллера
            HomeController homeController = new HomeController(mockTovar.Object, mockTitle.Object, mockCategory.Object);
            
            
            //Действие
            HomeViewModel result = (homeController.Index() as ViewResult)?.ViewData.Model as HomeViewModel;

            //Утверждение - что товаров, ктегорий и тайтлов по 2 штуки
            Assert.Equal(2, result.tovars.Count());
            Assert.Equal(2, result.categories.Count());
            Assert.Equal(2, result.titles.Count());

        }
        [Fact]
        public void Title_Index()
        {
            //Организация - создание имитированного хранилища
            Mock<ITovarService> mockTovar = new Mock<ITovarService>();
            Mock<ITitleService> mockTitle = new Mock<ITitleService>();
            Mock<ICategoryService> mockCategory = new Mock<ICategoryService>();

            mockTitle.Setup(m => m.Models()).Returns(new Titles[]
            {
                new Titles {TitlesId = 1, name="Атака титанов" , isPopular=true},
                new Titles {TitlesId= 2 , name="Наруто" , isPopular=true}
            });
            //Организация - создание контроллера
            HomeController homeController = new HomeController(mockTovar.Object, mockTitle.Object, mockCategory.Object);
            //Действие
            TitleViewModel[] result = GetViewMode<IEnumerable<TitleViewModel>>(homeController.TitleIndex())?.ToArray();
            //Утверждение - что тайтлов 2 штуки
            Assert.Equal(2, result.Length);
            //Утверждение - что второй товар называется Наруто
            Assert.Equal("Наруто", result[1].name);
        }
        /// <summary>
        /// Получаем данные из View
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        private T GetViewMode<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

    }
}
