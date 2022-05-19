using DiplomStore.BLL.DTO;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface ITovarService:IBaseService<TovarDTO>
    {
        TovarDTO Create(TovarDTO model);
        TovarDTO Edit(TovarDTO model);
        IEnumerable<TovarDTO> GetTovar();
    }
}
