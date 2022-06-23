using AutoMapper;
using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.Domain.Entity;
using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Order;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ITovarService Tovars;
        private readonly ITitleService Titles;
        private readonly ICategoryService Category;
        private readonly IOrderService Orders;
        private readonly UserManager<AppUser> userManager;
        public AdminController(ITovarService tv, ITitleService tit, ICategoryService cat, IOrderService _orders, 
            UserManager<AppUser> _userManager)
        {
            Tovars = tv;
            Titles = tit;
            Category = cat;
            Orders = _orders;
            userManager = _userManager;
        }
        public IActionResult Index() => View();
        public IActionResult IndexTovars()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            //сопоставление объектов Tovars в TovarViewModel                                    
            var tovars = map.Map<IEnumerable<Tovars>, List<TovarViewModel>>(Tovars.Models());

            return View(tovars);
        }
        public IActionResult IndexTitles()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов Titles в TitleViewModel                                    
            var title = map.Map<IEnumerable<Titles>, List<TitleViewModel>>(Titles.Models());
            return View(title);
        }
        public IActionResult IndexCategories()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            //сопоставление объектов Categories в CategoryViewModel                                    
            var category = map.Map<IEnumerable<Categories>, List<CategoryViewModel>>(Category.Models());
            return View(category);
        }

        public IActionResult IndexOrders()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Order, OrderViewModel>()).CreateMapper();
            //сопоставление объектов Order в OrderViewModel                                    
            var order = map.Map<IEnumerable<Order>, List<OrderViewModel>>(Orders.Orders());
            return View(order);
        }

        public IActionResult IndexUsers()
        {
            return View(userManager.Users.ToList());
        }

        // GET: AdminController/Create
        public ActionResult CreateTovar()
        {
            //Получаем лист всех тайтлов
            ViewBag.Titles = new SelectList(Titles.Models().Select(x => x.name).Distinct());
            //Получаем лист всех категорий
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
                model.TitleId = title.TitlesId;
            }            

            if(model.nameCategory != null)
            {
                var category = Category.Models().FirstOrDefault(n => n.name == model.nameCategory);
                model.CategoryId = category.CategoriesId;
            }            

            if (ModelState.IsValid)
            {
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TovarViewModel, Tovars>()).CreateMapper();
                //сопоставление объектов TovarViewModel в Tovars
                var tovars = map.Map<TovarViewModel, Tovars>(model);
                Tovars.Create(tovars);
              
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
            if(model.isPopular == true & Titles.Models().Where(p=>p.isPopular == true).Count() == 10)
            {
                ModelState.AddModelError("", "Популярных тайтлов больше 10");
            }
            if (ModelState.IsValid)
            {
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TitleViewModel, Titles>()).CreateMapper();
                //сопоставление объектов TitleViewModel в Titles
                var project = map.Map<TitleViewModel, Titles>(model);
                Titles.Create(project);
                return RedirectToAction("IndexTitles", model);
            }
            return View("Creates/CreateTitle");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            if (model.IsPopular == true & Category.Models().Where(p => p.IsPopular == true).Count() == 5)
            {
                ModelState.AddModelError("", "Популярных категорий больше 5");
            }
            if (ModelState.IsValid)
            {
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<CategoryViewModel, Categories>()).CreateMapper();
                //сопоставление объектов CategoryViewModel в Categories
                var project = map.Map<CategoryViewModel, Categories>(model);
                Category.Create(project);
                return RedirectToAction("IndexCategories", model);
            }
            return View("Creates/CreateCategory");
   
        }

        // GET: AdminController/Edit/5
        public ActionResult EditTovar(int id)
        {
            var tovar = Tovars.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<Tovars, TovarViewModel>()).CreateMapper();
            var tovarId = mapper.Map<Tovars, TovarViewModel>(tovar);
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
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TovarViewModel, Tovars>()).CreateMapper();
                //сопоставление объектов TovarViewModel в Tovars
                var project = map.Map<TovarViewModel, Tovars>(model);
                Tovars.Edit(project);
                return RedirectToAction("IndexTovars");
            }
            return View("Edits/EditTovar");
        }

        public ActionResult EditTitle(int id)
        {
            var title = Titles.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            var titleId = mapper.Map<Titles, TitleViewModel>(title);

            return View("Edits/EditTitle", titleId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTitle(TitleViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<TitleViewModel, Titles>()).CreateMapper();
                //сопоставление объектов TitleViewModel в Titles
                var project = map.Map<TitleViewModel, Titles>(model);
                Titles.Edit(project);
                return RedirectToAction("IndexTitles");
            }
            return View("Edits/EditTitle");
        }
        public ActionResult EditCategory(int id)
        {
            var category = Category.GetById(id);

            var mapper = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            var categoryId = mapper.Map<Categories, CategoryViewModel>(category);

            return View("Edits/EditCategory", categoryId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel model)
        {
             if (ModelState.IsValid)
            {
                //конфигурацию маппера, для сопоставления типов данных
                var map = new MapperConfiguration(c => c.CreateMap<CategoryViewModel, Categories>()).CreateMapper();
                //сопоставление объектов CategoryViewModel в Categories
                var project = map.Map<CategoryViewModel, Categories>(model);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int id)
        {
            Orders.Delete(id);
            return RedirectToAction("IndexOrders");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            return RedirectToAction("IndexUsers");
        }

        public IActionResult ListTitleTovars(int id)
        {
            var title = Titles.GetById(id);
            var map = new MapperConfiguration(c => c.CreateMap<Titles, TitleViewModel>()).CreateMapper();
            //сопоставление объектов Titles в TitleViewModel                                    
            var titles = map.Map<Titles, TitleViewModel>(title);
            return View(titles);
        }
        public IActionResult ListCategoryTovars(int id)
        {
            var category = Category.GetById(id);
            var map = new MapperConfiguration(c => c.CreateMap<Categories, CategoryViewModel>()).CreateMapper();
            ///сопоставление объектов Categories в CategoryViewModel                                    
            var categories = map.Map<Categories, CategoryViewModel>(category);
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTovarsCategory(int idCategory, int idTovars)
        {
            var tovar = Tovars.GetById(idTovars);
            Category.DeleteTovar(idCategory, tovar);
            return RedirectToAction("IndexCategories");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTovarsTitle(int idTitle, int idTovars)
        {
            var tovar = Tovars.GetById(idTovars);
            Titles.DeleteTovar(idTitle, tovar);
            return RedirectToAction("IndexCategories");
        }
    }
}
