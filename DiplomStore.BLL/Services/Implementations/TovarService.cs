using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.BLL.Services.Implementations
{
    public class TovarService : ITovarService
    {
        private readonly IUnitOfWork db;
        public TovarService(IUnitOfWork context)
        {
            db= context;
        }

        public Tovars Create(Tovars model)
        {          
            db.Tovars.Create(model);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {            
            db.Tovars.Delete(id);
            db.Save();
        }

        public Tovars Edit(Tovars model)
        {
            db.Tovars.Edit(model);
            db.Save();
            return model;
        }

        public Tovars GetById(int id)
        {
            ///Заполняем переменную сотрудниками по Id, которые связанны с проектомами
            var tovar = db.Tovars.GetAll.Include(e => e.category).Include(s=>s.titles).FirstOrDefault(p => p.TovarsId == id);
            return tovar;
        }

        public IEnumerable<Tovars> Models()
        { 
            return db.Tovars.GetAll.Include(e => e.category).Include(s => s.titles);
        }
        public IEnumerable<Tovars> GetTovar()
        {
            return db.Tovars.GetAll; 
        }
       
    }
}
