using ApiHW.Entities.Base;
using System.Linq.Expressions;

namespace ApiHW.Repositories.Interfaces
{
    public interface IRepository<T>where T: BaseEntity,new()
    {
        IQueryable<T> GetAllOrderAsync(Expression<Func<T, bool>>? expression=null, Expression<Func<T,object>>? orderExpression=null,
            bool isDesc = false,
            int skip=0,
            int take = 0, bool isTracking = true,
            params string[]includes);

        IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null,
            int skip = 0,
            int take = 0, bool isTracking = true,
            params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        Task SaveChangesAsync();
    }
}
