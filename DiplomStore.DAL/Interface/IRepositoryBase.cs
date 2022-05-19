namespace DiplomStore.DAL.Interface
{
    public interface IRepositoryBase<T>where T : class
    {
        T Edit(T entity);
        T Create(T entity);
        IQueryable<T> GetAll { get; }

        void Delete(int Id);
        
    }
}
