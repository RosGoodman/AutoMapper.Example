namespace AutoMapperExample.DAL.Repositoryes
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Delete(int id);
        void Update(T item);
        void Create(T item);
        void Save();
    }
}
