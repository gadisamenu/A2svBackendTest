namespace Application.Contracts.Persistance
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync(int page=1, int limit=10);

        Task<T> AddAsync(T entity);
        Task<bool> Exists(long d);
        Task<int> Count();
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }

}