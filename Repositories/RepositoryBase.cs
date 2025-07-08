using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Contracts;

namespace Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()
{
    protected RepositoryContext _context;

    protected RepositoryBase(RepositoryContext context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges
            ? _context.Set<T>()
            : _context.Set<T>().AsNoTracking();
    }

    public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges
            ? _context.Set<T>().Where(expression).SingleOrDefault()
            : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
            
    }

    public EntityEntry<T> Add(T entity)
    {
       return _context.Set<T>().Add(entity);
    }

    public EntityEntry<T> Update(T entity)
    {
        return _context.Set<T>().Update(entity);
    }

    public EntityEntry<T> Delete(T entity)
    {
        return _context.Set<T>().Remove(entity);
    }
}