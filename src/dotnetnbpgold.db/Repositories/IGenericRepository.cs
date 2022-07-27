using dotnetnbpgold.db.Entities;

namespace dotnetnbpgold.db.Repositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        Task<IList<T>> GetAllAsync();
    }
}