using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp2.Model.Entity;
using WebApp2.Model.ResponseModel;
namespace WebApp2.DataAccessLayer.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task<IQueryable<T>> GetAllAsQueryable();
        Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        //Task<MemberDetailsResponseModel> AddAsync(MemberDetailsResponseModel entity, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        T Update(T entity);
    }
}
