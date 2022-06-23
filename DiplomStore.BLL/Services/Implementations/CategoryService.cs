using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.BLL.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork db;
        public CategoryService(IUnitOfWork context)
        {
            db = context;
        }

        public Categories Create(Categories model)
        {            
            db.Categories.Create(model);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {
            db.Categories.Delete(id);
            db.Save();
        }

        public Categories Edit(Categories model)
        {
      
            db.Categories.Edit(model);
            db.Save();
            return model;
        }

        public Categories GetById(int id)
        {
            var category = db.Categories.GetAll.Include(e => e.tovars).FirstOrDefault(p => p.CategoriesId == id);                        
            return category;
        }

        public IEnumerable<Categories> Models()
        {           
            return db.Categories.GetAll;
        }
        public void DeleteTovar(int idCategory, Tovars tovar)
        {            
            var category = db.Categories.GetAll.FirstOrDefault(c=>c.CategoriesId == idCategory);            
            tovar.category.tovars.RemoveAll(p=>p.CategoryId == idCategory);
            db.Save();
        }
    }
}
