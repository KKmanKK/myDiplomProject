using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ITovarService:IBaseService<Tovars>
    {
        Tovars Create(Tovars model);
        Tovars Edit(Tovars model);
        IEnumerable<Tovars> GetTovar();
    }
}
