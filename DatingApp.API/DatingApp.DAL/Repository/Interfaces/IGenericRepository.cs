using DatingApp.Models.Database.DataModel;
using System.Linq.Expressions;

namespace DatingApp.DAL.Repository.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    T Add(T entity);
    Task<T> AddAsync(T entity);
    void Delete(T entity);
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> GetAll();
    IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
    bool SaveChanges();
    Task<bool> SaveChangesAsync();
}