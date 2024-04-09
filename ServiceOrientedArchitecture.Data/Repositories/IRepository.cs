using Microsoft.EntityFrameworkCore;
using ServiceOrientedArchitecture.Common.Models;

namespace ServiceOrientedArchitecture.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Set();

    IQueryable<T> AsNoTracking();

    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
