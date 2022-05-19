using DiplomStore.BLL.DTO;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ITitleService:IBaseService<TitleDTO>
    {
        TitleDTO Create(TitleDTO model);
        TitleDTO Edit(TitleDTO model);
    }
}
