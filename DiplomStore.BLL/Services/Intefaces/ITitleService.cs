using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ITitleService:IBaseService<Titles>
    {
        Titles Create(Titles model);
        void DeleteTovar(int idTitle, Tovars tovar);
        Titles Edit(Titles model);
    }
}
