using DiplomStore.BLL.DTO;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ICategoryService:IBaseService<CategoryDTO>
    {
        CategoryDTO Create(CategoryDTO model);

        CategoryDTO Edit(CategoryDTO model);
    }
}
