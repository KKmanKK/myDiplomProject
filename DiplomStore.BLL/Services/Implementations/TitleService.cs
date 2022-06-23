using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.BLL.Services.Implementations
{
    public class TitleService : ITitleService
    {
        private readonly IUnitOfWork db;
        public TitleService(IUnitOfWork context)
        {
            db = context;
        }

        public Titles Create(Titles model)
        {           
            db.Titles.Create(model);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {
            db.Titles.Delete(id);
            db.Save();
        }

        public Titles Edit(Titles model)
        {
            db.Titles.Edit(model);
            db.Save();
            return model;
        }

        public Titles GetById(int id)
        {
            var title = db.Titles.GetAll.Include(e => e.tovars).FirstOrDefault(p => p.TitlesId == id);
            return title;
        }

        public IEnumerable<Titles> Models()
        {            
            return db.Titles.GetAll;
        }
        public void DeleteTovar(int idTitle, Tovars tovar)
        {
            var title = db.Titles.GetAll.FirstOrDefault(c => c.TitlesId == idTitle);
            tovar.category.tovars.RemoveAll(p => p.TitleId == idTitle);
            db.Save();
        }
    }
}
