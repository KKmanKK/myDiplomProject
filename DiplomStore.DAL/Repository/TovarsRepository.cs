using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Repository
{
    public class TovarsRepository : ITovarsRepository
    {
        private readonly AppDbContext context;
        public TovarsRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Tovars> GetAll => context.tovars;

        public Tovars Create(Tovars entity)
        {
            var title = context.titles.FirstOrDefault(x => x.TitlesId == entity.TitleId);
            var category = context.categories.FirstOrDefault(x => x.CategoriesId == entity.CategoryId);
            context.tovars.Add(entity);
            title.tovars.Add(entity);
            category.tovars.Add(entity);
            return entity;
            
        }

        public void Delete(int Id)
        {            
            if(Id != 0)
            {
                var tovar = context.tovars.FirstOrDefault(t => t.TovarsId == Id);
                context.tovars.Remove(tovar);
            }
        }

        public Tovars Edit(Tovars entity)
        {
            var tovar = context.tovars.FirstOrDefault(t => t.TovarsId == entity.TovarsId);
            if(tovar != null)
            {
                tovar.name = entity.name;
                tovar.price = entity.price;
                tovar.isValids = entity.isValids;
                tovar.hight = entity.hight;
                tovar.width = entity.width;
                tovar.count = entity.count;
                tovar.NameImg = entity.NameImg;
                tovar.materials = entity.materials;
                tovar.filler = entity.filler;
            }
            return entity;
        }
    }
}
