using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ICategoryService:IBaseService<Categories>
    {
        Categories Create(Categories model);
        void DeleteTovar(int idCategory, Tovars tovar);
        Categories Edit(Categories model);
    }
}
