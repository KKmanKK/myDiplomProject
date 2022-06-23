using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Repository
{
    public class CategoriesRepository:IRepositoryBase<Categories>
    {
        private readonly AppDbContext context;
        public CategoriesRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Categories> GetAll => context.categories;

        public Categories Create(Categories entity)
        {
            context.categories.Add(entity);
            return entity;

        }

        public void Delete(int Id)
        {
            var category = context.categories.FirstOrDefault(t => t.CategoriesId == Id);

            if (Id != 0)
            {
                context.categories.Remove(category);
            }
        }

        public Categories Edit(Categories entity)
        {
            var category = context.categories.FirstOrDefault(c=>c.CategoriesId == entity.CategoriesId);
            if (category != null)
            {
                category.name = entity.name;
                category.NameImg = entity.NameImg;
                category.tovars = entity.tovars;
                category.IsPopular = entity.IsPopular;
            }
            return entity;
        }
    }
}
