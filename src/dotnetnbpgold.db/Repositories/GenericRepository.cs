using dotnetnbpgold.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetnbpgold.db.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private protected readonly DBContext _context;  
        private DbSet<T> _entities;

        public GenericRepository(DBContext context)
        {  
            _context = context;  
            _entities = context.Set<T>();  
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}