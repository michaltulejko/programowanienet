using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Database.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatingApp.DAL.Repository;

public class BaseRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected readonly DataContext Context;

    public BaseRepository(DataContext context)
    {
        Context = context;
    }

    public virtual T Add(T entity)
    {
        Context.Add(entity);
        return entity;
    }

    public virtual Task<T> AddAsync(T entity)
    {
        Context.AddAsync(entity);
        return Task.FromResult(entity);
    }

    public virtual void Delete(T entity)
    {
        Context.Remove(entity);
    }

    public virtual T? GetById(int id)
    {
        var entity = Context.Set<T>().FirstOrDefault(x => x.Id == id);
        return entity;
    }

    public virtual Task<T?> GetByIdAsync(int id)
    {
        var entity = Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public virtual IQueryable<T> GetAll()
    {
        var query = Context.Set<T>().AsQueryable();

        return query;
    }

    public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
    {
        var query = Context.Set<T>().AsQueryable().Where(expression);

        return query;
    }

    public virtual bool SaveChanges()
    {
        return Context.SaveChanges() > 0;
    }

    public virtual async Task<bool> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync() > 0;
    }
}