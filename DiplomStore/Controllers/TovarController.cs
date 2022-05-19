using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL.Interface;
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

        public IActionResult TovarAndCategoryList(int CategoriesId)
        {
            
            TovarAndCategoryListViewModel TovarCategory = new TovarAndCategoryListViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());            
            TovarCategory.Tovar = tovars.Where(p => p.category.CategoriesId == CategoriesId).ToList(); 


            var cat = Categories.Models().FirstOrDefault(p=>p.CategoriesId == CategoriesId);
            var mapCategor = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var categor = mapCategor.Map<CategoryDTO, CategoryViewModel>(cat);


            var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                      
            TovarCategory.Titles = mapTile.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models().Where(p => p.tovars.Any(d => d.isValids) && p.tovars.Any(d=>d.CategoryId == CategoriesId)));

            TovarCategory.Category = categor;
            return View(TovarCategory);
        }
       [HttpGet]
        public IActionResult TovarAndCategoryList(string searhString, int CategoriesId, int[] selectedTitle)
        {
            TovarAndCategoryListViewModel TovarCategory = new TovarAndCategoryListViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());
            TovarCategory.Tovar = tovars.Where(p => p.category.CategoriesId == CategoriesId).ToList();

            var cat = Categories.Models().FirstOrDefault(p => p.CategoriesId == CategoriesId);
            var mapCategor = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var categor = mapCategor.Map<CategoryDTO, CategoryViewModel>(cat);

            var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel          

            TovarCategory.Titles = mapTile.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models().Where(p => p.tovars.Any(d => d.isValids) && p.tovars.Any(d => d.CategoryId == CategoriesId)));

            TovarCategory.Category = categor;
            List<TovarViewModel> t = new List<TovarViewModel>();
            if (!string.IsNullOrEmpty(searhString))
            {
                if(selectedTitle != null)
                {
                    foreach (var c in Titles.Models().Where(p => selectedTitle.Contains(p.TitlesId)))
                    {
                        t.AddRange(tovars.Where(p => p.category.CategoriesId == CategoriesId & p.TitleId == c.TitlesId).ToList());

                    }
                    if (t.Count > 0)
                    {
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

                //TovarCategory.Tovar = tovars.Where(p=>p.name.ToLower().Contains(searhString.ToLower()) & p.category.CategoriesId == CategoriesId & p.TitleId==).ToList();
               
            }
            return View("TovarAndCategoryList",TovarCategory);
        }


        public IActionResult TovarAndTitleList(int TitleId)
        {

            TovarAndTitleViewModel TovarTitle = new TovarAndTitleViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());

            TovarTitle.tovars = tovars.Where(p => p.titles.TitlesId == TitleId).ToList();


            var tit = Titles.Models().FirstOrDefault(p => p.TitlesId == TitleId);
            var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                      
            TovarTitle.title = mapTile.Map<TitleDTO, TitleViewModel>(tit);

           
            return View(TovarTitle);
        }
        [HttpGet]
        public IActionResult TovarAndTitleList(int TitleId , string searhString)
        {
            TovarAndTitleViewModel TovarTitle = new TovarAndTitleViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());

            var tit = Titles.Models().FirstOrDefault(p => p.TitlesId == TitleId);
            var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();

            if (!string.IsNullOrEmpty(searhString))
            {              
                TovarTitle.tovars = tovars.Where(p => p.titles.TitlesId == TitleId & p.name.Contains(searhString)).ToList();               
                ///сопоставление объектов TovarDTO в TovarListViewModel                      
                TovarTitle.title = mapTile.Map<TitleDTO, TitleViewModel>(tit);


                return View(TovarTitle);
            }
            TovarTitle.tovars = tovars.Where(p => p.titles.TitlesId == TitleId).ToList();
            ///сопоставление объектов TovarDTO в TovarListViewModel                      
            TovarTitle.title = mapTile.Map<TitleDTO, TitleViewModel>(tit);
            return View(TovarTitle);
        }

        public IActionResult TovarIndex(int TovarId)
        {
            TovarsTitleViewModel TovarTitle = new TovarsTitleViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovar = map.Map<TovarDTO, TovarViewModel>(Tovars.Models().FirstOrDefault(p=>p.TovarsId == TovarId ));
            TovarTitle.Tovar = tovar;
            var c = Titles.Models().FirstOrDefault(t=>t.TitlesId == tovar.TitleId);

            var maptitle = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = maptitle.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models().Where(t => t.TitleId == tovar.TitleId && t.TovarsId !=TovarId));

            TovarTitle.Tovars = tovars;
            return View(TovarTitle);

        }
        [HttpGet]
        public IActionResult IndexTovarOfTitle(int[] selectedTitle,int CategoriesId)
        {
            
            TovarAndCategoryListViewModel TovarCategory = new TovarAndCategoryListViewModel();
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());
            List<TovarViewModel> t = new List<TovarViewModel>();
            if(selectedTitle != null)
            {
                foreach (var c in Titles.Models().Where(p => selectedTitle.Contains(p.TitlesId)))
                {
                    t.AddRange(tovars.Where(p => p.category.CategoriesId == CategoriesId & p.TitleId == c.TitlesId).ToList());

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
            
            var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var Title = mapTile.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models().Where(p => p.tovars.Any(d => d.isValids) && p.tovars.Any(d => d.CategoryId == CategoriesId)));
           
            TovarCategory.Titles = Title;

            var cat = Categories.Models().FirstOrDefault(p => p.CategoriesId == CategoriesId);
            var mapCategor = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarListViewModel                                    
            var categor = mapCategor.Map<CategoryDTO, CategoryViewModel>(cat);
            TovarCategory.Category = categor;
            
           

            return View("TovarAndCategoryList",TovarCategory);

        }

        [HttpGet]
        public IActionResult TovarSearch(string searhString)
        {
            if (!string.IsNullOrEmpty(searhString))
            {
                TovarSearchViewModel tovarSearch = new TovarSearchViewModel();
                var mapTovar = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
                ///сопоставление объектов TovarDTO в TovarListViewModel                                    
                var tovar = mapTovar.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models().Where(p=>p.name.ToLower().Contains(searhString.ToLower())));
                
                    tovarSearch.Tovars = tovar;
                    tovarSearch.searchString = searhString;
                    return View(tovarSearch);
                
                
            }
            var map = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var category = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(Categories.Models());
            return View("CategoryIndex", category);
        }

        //[HttpPost]
        //public IActionResult SearchTitle(string searh)
        //{
        //    var mapTile = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
        //    ///сопоставление объектов TovarDTO в TovarListViewModel                                    
        //    var Title = mapTile.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models());            

        //    if (string.IsNullOrEmpty(searh))
        //    {
        //        return View()
        //    }
        //}


    }
}
