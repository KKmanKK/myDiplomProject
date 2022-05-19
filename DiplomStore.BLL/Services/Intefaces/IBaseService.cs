namespace DiplomStore.BLL.Services.Intefaces
{
    public interface IBaseService<T>where T : class
    {
        IEnumerable<T> Models();
        void Delete(int id);
        T GetById(int id);    
    }
}
