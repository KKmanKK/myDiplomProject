using AutoMapper;
using DiplomStore.BLL.DTO;
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

        public TitleDTO Create(TitleDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<TitleDTO, Titles>()).CreateMapper();
            ///сопоставление объектов TitleDTO в Titles
            var titles = map.Map<TitleDTO, Titles>(model);
            db.Titles.Create(titles);
            db.Save();
            return model;
        }

        public void Delete(int id)
        {
            db.Titles.Delete(id);
            db.Save();
        }

        public TitleDTO Edit(TitleDTO model)
        {
            var map = new MapperConfiguration(c => c.CreateMap<TitleDTO, Titles>()).CreateMapper();
            ///сопоставление объектов TitleDTO в Titles
            var titles = map.Map<TitleDTO, Titles>(model);
            db.Titles.Edit(titles);
            db.Save();
            return model;
        }

        public TitleDTO GetById(int id)
        {
            var title = db.Titles.GetAll.Include(e => e.tovars).FirstOrDefault(p => p.TitlesId == id);

            ///конфигурацию маппера, для сопоставления типов данных
            var mapper = new MapperConfiguration(c => c.CreateMap<Titles, TitleDTO>()).CreateMapper();
            ///сопоставление объектов Titles в TitleDTO
            var empl = mapper.Map<Titles, TitleDTO>(title);

            return empl;
        }

        public IEnumerable<TitleDTO> Models()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Titles, TitleDTO>()).CreateMapper();
            return map.Map<IQueryable<Titles>, List<TitleDTO>>(db.Titles.GetAll);
        }
    }
}
