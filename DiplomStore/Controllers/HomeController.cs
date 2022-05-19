using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.ViewsModels;
using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;
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
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, ListTovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<ListTovarViewModel>>(Tovars.Models());
            
            model.tovars = tovars;

            var pop = Titles.Models().Where(p => p.isPopular == true);
            var map2 = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TitleDTO в TitlesViewModels
            var titles = map2.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(pop);
            if(titles.Any(p=>p.isPopular == true))
            {
                model.titles = titles;
            }


            var mapCate = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var categors = mapCate.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(Category.Models().Where(p=>p.IsPopular == true));
            model.categories = categors;

            return View(model);
        }

        public IActionResult CategoryIndex()
        {
            var map = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var tovars = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(Category.Models());

            return View(tovars);
        }

        public IActionResult TitleIndex()
        {
            var map = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var titles = map.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models());

            return View(titles);
        }
    }
}
