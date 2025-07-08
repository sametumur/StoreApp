using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repositories.Contracts;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> FindAll(bool trackChanges);
    T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    EntityEntry<T> Add(T entity);
    EntityEntry<T> Update(T entity);
    EntityEntry<T> Delete(T entity);
}