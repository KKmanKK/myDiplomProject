using AutoMapper;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Mvc;

namespace DiplomStore.Controllers
{
    public class TovarController:Controller
    {
        private readonly ITovarService Tovars;
        private readonly ICategoryService Categories;
        private readonly ITitleService Titles;
        public TovarController(ITovarService ctx, ICategoryService categories, ITitleService titles)
        {
            Tovars = ctx;
            Categories = categories;
            Titles = titles;
        }
    /// <summary>
    /// Выводит товары связанные с категориями
    /// </summary>
    /// <param name="searhString">Поисковая строка</param>
    /// <param name="CategoriesId"></param>
    /// <param name="selectedTitle">Выбранный тайтл</param>
    /// <param name="page"></param>
    /// <returns></returns>
        [HttpGet]
        public IActionResult TovarAndCategoryList(string searhString, int CategoriesId, int[] selectedTitle, int page = 1)
        {
            TovarAndCategoryListViewModel TovarCategory = new TovarAndCategoryListViewModel();
            
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов Tovars в TovarViewModel                                    
            var tovars = map.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models());
            //Находим товары связанные с категориями
            TovarCategory.Tovar = tovars.Where(p => p.category.CategoriesId == CategoriesId).ToList();
            //Всего товаров на странице
            int pageSize = 6;
            //Всего товаров
            var count = tovars.Count();
            //Получение товаров
            var item = tovars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(item, pageViewModel);
            TovarCategory.IndexViewModel = viewModel;
            
            var cat = Categories.Models().FirstOrDefault(p => p.CategoriesId == CategoriesId);
            var mapCategor = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов Categories в CategoryViewModel                                    
            var categor = mapCategor.Map<Categories, CategoryViewModel>(cat);

            var mapTile = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов Titles в TitleViewModel          
            TovarCategory.Titles = mapTile.Map<IEnumerable<Titles>, List<TitleViewModel>>(Titles.Models().Where(p => p.tovars.Any(d => d.isValids) && p.tovars.Any(d => d.CategoryId == CategoriesId)));

            TovarCategory.Category = categor;
            List<TovarViewModel> t = new List<TovarViewModel>();
            if (!string.IsNullOrEmpty(searhString))
            {
                if(selectedTitle != null)
                {
                    foreach (var c in Titles.Models().Where(p => selectedTitle.Contains(p.TitlesId)))
                    {
                        // Добавляем в лист товары, связанных одной категорией или тайтлом
                        t.AddRange(item.Where(p => p.category.CategoriesId == CategoriesId & p.TitleId == c.TitlesId).ToList());

                    }
                    if (t.Count > 0)
                    {
                        //получаем товар по поисковой строке
                        TovarCategory.Tovar = t.Where(p=>p.name.ToLower().Contains(searhString.ToLower())).ToList();
                        TovarCategory.selectTitle.AddRange(selectedTitle);
                        return View("TovarAndCategoryList", TovarCategory);
                    }
                    else
                    {

                        t.AddRange(tovars.Where(p => p.name.ToLower().Contains(searhString.ToLower()) & p.category.CategoriesId == CategoriesId));
                        TovarCategory.Tovar = t;
                    }
                }

                
               
            }
            TovarCategory.Tovar = item;
            return View("TovarAndCategoryList",TovarCategory);
        }
        /// <summary>
        /// Выводит товары связанные с тайтлами
        /// </summary>
        /// <param name="TitleId">Поисковая строка</param>
        /// <param name="searhString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TovarAndTitleList(int TitleId , string searhString, int page=1)
        {
            TovarAndTitleViewModel TovarTitle = new TovarAndTitleViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models());

            var tit = Titles.Models().FirstOrDefault(p => p.TitlesId == TitleId);
            var mapTile = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();

            if (!string.IsNullOrEmpty(searhString))
            {   
                //Получаем товары по поисковой строке
                TovarTitle.tovars = tovars.Where(p => p.titles.TitlesId == TitleId & p.name.Contains(searhString)).ToList();
                //сопоставление объектов Titles в TitleViewModel                      
                TovarTitle.title = mapTile.Map<Titles, TitleViewModel>(tit);
                return View(TovarTitle);
            }
            int pageSize = 6;
            var count = tovars.Count();
            var item = tovars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(item, pageViewModel);
            TovarTitle.IndexViewModel = viewModel;

            TovarTitle.tovars = item.Where(p => p.titles.TitlesId == TitleId).ToList();
            ///сопоставление объектов Titles в TitleViewModel                      
            TovarTitle.title = mapTile.Map<Titles, TitleViewModel>(tit);
            return View(TovarTitle);
        }

        public IActionResult TovarIndex(int TovarId)
        {
            TovarsTitleViewModel TovarTitle = new TovarsTitleViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов Tovars в TovarViewModel                                    
            var tovar = map.Map<Tovars, TovarViewModel>(Tovars.Models().FirstOrDefault(p=>p.TovarsId == TovarId ));
            TovarTitle.Tovar = tovar;
            var c = Titles.Models().FirstOrDefault(t=>t.TitlesId == tovar.TitleId);

            var maptitle = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов Tovars в TovarViewModel                                    
            var tovars = maptitle.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models().Where(t => t.TitleId == tovar.TitleId && t.TovarsId !=TovarId));

            TovarTitle.Tovars = tovars;
            return View(TovarTitle);

        }
        /// <summary>
        /// Выводит товары свзяанные с категорией и выбраннысм тайтлами
        /// </summary>
        /// <param name="selectedTitle"></param>
        /// <param name="CategoriesId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IndexTovarOfTitle(int[] selectedTitle,int CategoriesId, int page = 1)
        {
            
            TovarAndCategoryListViewModel TovarCategory = new TovarAndCategoryListViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models());

            int pageSize = 6;
            var count = tovars.Count();
            var item = tovars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(item, pageViewModel);
            TovarCategory.IndexViewModel = viewModel;

            List<TovarViewModel> t = new List<TovarViewModel>();
            if(selectedTitle != null)
            {
                foreach (var c in Titles.Models().Where(p => selectedTitle.Contains(p.TitlesId)))
                {
                    t.AddRange(item.Where(p => p.category.CategoriesId == CategoriesId & p.TitleId == c.TitlesId).ToList());

                }
                if(t.Count >0)
                {
                    TovarCategory.Tovar = t;
                    TovarCategory.selectTitle.AddRange(selectedTitle);
                }
                else
                {
                    t.AddRange(tovars.Where(p => p.category.CategoriesId == CategoriesId));
                    TovarCategory.Tovar = t;
                }
                
            }
          
            var mapTile = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов TovarDTO в TovarListViewModel                                    
            var Title = mapTile.Map<IEnumerable<Titles>, List<TitleViewModel>>(Titles.Models().Where(p => p.tovars.Any(d => d.isValids) && p.tovars.Any(d => d.CategoryId == CategoriesId)));
           
            TovarCategory.Titles = Title;

            var cat = Categories.Models().FirstOrDefault(p => p.CategoriesId == CategoriesId);
            var mapCategor = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов TovarDTO в TovarListViewModel                                    
            var categor = mapCategor.Map<Categories, CategoryViewModel>(cat);
            TovarCategory.Category = categor;                       

            return View("TovarAndCategoryList",TovarCategory);

        }
        /// <summary>
        /// Выводит товары, которые соответствуют поисковой строке
        /// </summary>
        /// <param name="searhString"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TovarSearch(string searhString)
        {
            if (!string.IsNullOrEmpty(searhString))
            {
                TovarSearchViewModel tovarSearch = new TovarSearchViewModel();
                var mapTovar = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
                //сопоставление объектов TovarDTO в TovarListViewModel                                    
                var tovar = mapTovar.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models().Where(p=>p.name.ToLower().Contains(searhString.ToLower())));
                
                    tovarSearch.Tovars = tovar;
                    tovarSearch.searchString = searhString;
                    return View(tovarSearch);
                
                
            }
            var map = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов TovarDTO в TovarViewModel                                    
            var category = map.Map<IEnumerable<Categories>, List<CategoryViewModel>>(Categories.Models());
            return View("CategoryIndex", category);
        }
        

    }
}
