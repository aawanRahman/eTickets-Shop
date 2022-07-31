
using eTickets.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges(); ;
        }

        public IEnumerable<T> GetAllData()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAllData(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.ToList();
        }

        public async Task<T> GetDataById(int id)
        {
            var actorData = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return actorData;
        }

        public async Task Remove(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
           await _context.SaveChangesAsync();
        }

        public async Task Update(int id, T entity)
        {

            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            

        }
    }
}
