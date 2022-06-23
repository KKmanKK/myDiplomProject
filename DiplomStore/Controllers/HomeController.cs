using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels;
using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiplomStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITovarService Tovars;
        private readonly ITitleService Titles;
        private readonly ICategoryService Category;    
        public HomeController(ITovarService ctx, ITitleService titles, ICategoryService category)
        {
            Tovars = ctx;
            Titles = titles;
            Category = category;           
        }
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, ListTovarViewModel>()).CreateMapper();
            //сопоставление объектов Tovars в ListTovarViewModel                                    
            var tovars = map.Map<IEnumerable<Tovars>, List<ListTovarViewModel>>(Tovars.Models());
            
            model.tovars = tovars;

            var pop = Titles.Models().Where(p => p.isPopular == true);
            var map2 = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов Titles в TitleViewModel
            var titles = map2.Map<IEnumerable<Titles>, List<TitleViewModel>>(pop);
            
                model.titles = titles.Where(p=>p.isPopular == true);                        

            var mapCate = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов Categories в CategoryViewModel                                    
            var categors = mapCate.Map<IEnumerable<Categories>, List<CategoryViewModel>>(Category.Models().Where(p=>p.IsPopular == true));
            model.categories = categors;

            return View(model);
        }

        public IActionResult CategoryIndex()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов Categories в CategoryViewModel                                    
            var tovars = map.Map<IEnumerable<Categories>, List<CategoryViewModel>>(Category.Models());

            return View(tovars);
        }

        public IActionResult TitleIndex()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов Titles в TitleViewModel                                    
            var titles = map.Map<IEnumerable<Titles>, List<TitleViewModel>>(Titles.Models());

            return View(titles);
        }
    }
}
