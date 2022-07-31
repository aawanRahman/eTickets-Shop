using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IEnumerable<T> GetAllData();
        IEnumerable<T> GetAllData(params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetDataById(int id);
        void Add(T entity);
        Task Remove(int id);
        Task Update(int id, T entity);
    }
}
