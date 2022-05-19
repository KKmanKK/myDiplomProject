using AutoMapper;
using DiplomStore.BLL.DTO;
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

        public CategoryDTO Create(CategoryDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<CategoryDTO, Categories>()).CreateMapper();
            ///сопоставление объектов TitleDTO в Titles
            var categories = map.Map<CategoryDTO, Categories>(model);
            db.Categories.Create(categories);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {
            db.Categories.Delete(id);
            db.Save();
        }

        public CategoryDTO Edit(CategoryDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<CategoryDTO, Categories>()).CreateMapper();
            ///сопоставление объектов TitleDTO в Titles
            var categories = map.Map<CategoryDTO, Categories>(model);
            db.Categories.Edit(categories);
            db.Save();
            return model;
        }

        public CategoryDTO GetById(int id)
        {
            var category = db.Categories.GetAll.Include(e => e.tovars).FirstOrDefault(p => p.CategoriesId == id);

            ///конфигурацию маппера, для сопоставления типов данных
            var mapper = new MapperConfiguration(c => c.CreateMap<Categories, CategoryDTO>()).CreateMapper();
            ///сопоставление объектов Categories в CategoryDTO
            var empl = mapper.Map<Categories, CategoryDTO>(category);

            return empl;
        }

        public IEnumerable<CategoryDTO> Models()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Categories, CategoryDTO>()).CreateMapper();
            return map.Map<IQueryable<Categories>, List<CategoryDTO>>(db.Categories.GetAll);
        }
    }
}
