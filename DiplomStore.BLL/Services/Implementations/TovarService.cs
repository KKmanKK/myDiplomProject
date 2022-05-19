using AutoMapper;
using DiplomStore.BLL.DTO;
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

        public TovarDTO Create(TovarDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, Tovars>()).CreateMapper();
            ///сопоставление объектов TovarDTO в Tovars
            var tovars = map.Map<TovarDTO, Tovars>(model);
            db.Tovars.Create(tovars);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {            
            db.Tovars.Delete(id);
            db.Save();
        }

        public TovarDTO Edit(TovarDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<TovarDTO, Tovars>()).CreateMapper();
            ///сопоставление объектов TovarDTO в Tovars
            var tovars = map.Map<TovarDTO, Tovars>(model);
            db.Tovars.Edit(tovars);
            db.Save();
            return model;
        }

        public TovarDTO GetById(int id)
        {
            ///Заполняем переменнуб сотрудниками по Id, которые связанны с проектомами
            var tovar = db.Tovars.GetAll.Include(e => e.category).Include(s=>s.titles).FirstOrDefault(p => p.TovarsId == id);
           
            ///конфигурацию маппера, для сопоставления типов данных
            var mapper = new MapperConfiguration(c => c.CreateMap<Tovars, TovarDTO>()).CreateMapper();
            ///сопоставление объектов Tovars в TovarDTO
            var empl = mapper.Map<Tovars, TovarDTO>(tovar);

            return empl;
        }

        public IEnumerable<TovarDTO> Models()
        {
            var p = db.Tovars.GetAll.Include(e => e.category).Include(s => s.titles);
            var map = new MapperConfiguration(c=>c.CreateMap<Tovars,TovarDTO>()).CreateMapper();
            return map.Map<IQueryable<Tovars>, List<TovarDTO>>(p);
        }
        public IEnumerable<TovarDTO> GetTovar()
        {
            var p = db.Tovars.GetAll;
            var map = new MapperConfiguration(c => c.CreateMap<Tovars, TovarDTO>()).CreateMapper();
            return map.Map<IQueryable<Tovars>, List<TovarDTO>>(p);
        }
    }
}
