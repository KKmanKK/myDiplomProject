using AutoMapper;
using DiplomStore.BLL.DTO;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITovarService Tovars;
        private readonly ITitleService Titles;
        private readonly ICategoryService Category;
        public AdminController(ITovarService tv, ITitleService tit, ICategoryService cat)
        {
            Tovars = tv;
            Titles = tit;
            Category = cat;
        }
        public IActionResult Index() => View();
        public IActionResult IndexTovars()
        {
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            ///сопоставление объектов TovarDTO в TovarViewModel                                    
            var tovars = map.Map<IEnumerable<TovarDTO>, List<TovarViewModel>>(Tovars.Models());

            return View(tovars);
        }
        public IActionResult IndexTitles()
        {
            var map = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            ///сопоставление объектов TitleDTO в TitleViewModel                                    
            var title = map.Map<IEnumerable<TitleDTO>, List<TitleViewModel>>(Titles.Models());
            return View(title);
        }
        public IActionResult IndexCategories()
        {
            var map = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов CategoryDTO в CategoryViewModel                                    
            var category = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(Category.Models());
            return View(category);
        }                             
        

        // GET: AdminController/Create
        public ActionResult CreateTovar()
        {
            
            ViewBag.Titles = new SelectList(Titles.Models().Select(x => x.name).Distinct());
            ViewBag.Categories = new SelectList(Category.Models().Select(x => x.name).Distinct());
            return View("Creates/CreateTovar");
        }

        public ActionResult CreateTitle()
        {
            return View("Creates/CreateTitle");
        }
        public ActionResult CreateCategory()
        {
            return View("Creates/CreateCategory");
        }
        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTovar(TovarViewModel model)
        {
            if(model.nameTitle != null)
            {
                var title = Titles.Models().FirstOrDefault(n => n.name == model.nameTitle);
                ///конфигурацию маппера, для сопоставления типов данных
                var mapTitle = new MapperConfiguration(c => c.CreateMap<TitleDTO, Titles>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var modelTitle = mapTitle.Map<TitleDTO, Titles>(title);

                model.TitleId = modelTitle.TitlesId;
            }
            

            if(model.nameCategory != null)
            {
                var category = Category.Models().FirstOrDefault(n => n.name == model.nameCategory);
                ///конфигурацию маппера, для сопоставления типов данных
                var mapCategory = new MapperConfiguration(c => c.CreateMap<CategoryDTO, Categories>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var modelCategory = mapCategory.Map<CategoryDTO, Categories>(category);

                model.CategoryId = modelCategory.CategoriesId;
            }
            



            if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TovarViewModel, TovarDTO>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var project = map.Map<TovarViewModel, TovarDTO>(model);
                Tovars.Create(project);
              
                return RedirectToAction("IndexTovars", model);
            }
            ViewBag.Titles = new SelectList(Titles.Models().Select(x => x.name).Distinct());
            ViewBag.Categories = new SelectList(Category.Models().Select(x => x.name).Distinct());

            return View("Creates/CreateTovar");          

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTitle(TitleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TitleViewModel, TitleDTO>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var project = map.Map<TitleViewModel, TitleDTO>(model);
                Titles.Create(project);
                return RedirectToAction("IndexTitles", model);
            }
            return View("Creates/CreateTitle");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<CategoryViewModel, CategoryDTO>()).CreateMapper();
                ///сопоставление объектов CategoryViewModel в CategoryDTO
                var project = map.Map<CategoryViewModel, CategoryDTO>(model);
                Category.Create(project);
                return RedirectToAction("IndexCategories", model);
            }
            return View("Creates/CreateCategory");
   
        }

        // GET: AdminController/Edit/5
        public ActionResult EditTovar(int id)
        {
            var tovar = Tovars.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<TovarDTO, TovarViewModel>()).CreateMapper();
            var tovarId = mapper.Map<TovarDTO, TovarViewModel>(tovar);
            ViewBag.Titles = new SelectList(Titles.Models().Select(x => x.name).Distinct());
            ViewBag.Categories = new SelectList(Category.Models().Select(x => x.name).Distinct());
            return View("Edits/EditTovar", tovarId);
        }
     
        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTovar(TovarViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TovarViewModel, TovarDTO>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var project = map.Map<TovarViewModel, TovarDTO>(model);
                Tovars.Edit(project);
                return RedirectToAction("IndexTovars");
            }
            return View("Edits/EditTovar");
        }

        public ActionResult EditTitle(int id)
        {
            var title = Titles.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<TitleDTO, TitleViewModel>()).CreateMapper();
            var titleId = mapper.Map<TitleDTO, TitleViewModel>(title);

            return View("Edits/EditTitle", titleId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTitle(TitleViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TitleViewModel, TitleDTO>()).CreateMapper();
                ///сопоставление объектов TovarViewModel в TovarDTO
                var project = map.Map<TitleViewModel, TitleDTO>(model);
                Titles.Edit(project);
                return RedirectToAction("IndexTitles");
            }
            return View("Edits/EditTitle");
        }
        public ActionResult EditCategory(int id)
        {
            var category = Category.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var categoryId = mapper.Map<CategoryDTO, CategoryViewModel>(category);

            return View("Edits/EditCategory", categoryId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel model)
        {
             if (ModelState.IsValid)
            {
                ///конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<CategoryViewModel, CategoryDTO>()).CreateMapper();
                ///сопоставление объектов CategoryViewModel в CategoryDTO
                var project = map.Map<CategoryViewModel, CategoryDTO>(model);
                Category.Edit(project);
                return RedirectToAction("IndexCategories");
            }
            return View("Edits/EditCategory");
        }
      

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTovar(int id)
        {
           Tovars.Delete(id);
            return RedirectToAction("IndexTovars");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTitle(int id)
        {
            Titles.Delete(id);
            return RedirectToAction("IndexTitles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            Category.Delete(id);
            return RedirectToAction("IndexCategories");
        }
    }
}
