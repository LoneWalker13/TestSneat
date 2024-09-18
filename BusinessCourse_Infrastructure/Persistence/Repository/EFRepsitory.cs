using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public abstract class EFRepsitory<T> : IAsyncRepository<T> where T : AuditableEntity
  {
    protected readonly ApplicationDbContext _context;

    public EFRepsitory(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
      await _context.Set<T>().AddAsync(entity);

      return entity;
    }

    public async Task DeleteAsync(T entity)
    {
      _context.Set<T>().Remove(entity);
    }

    public async Task UpdateAsync(T entity)
    {
      _context.Set<T>().Update(entity);
    }

    public async Task UpdateAsync(List<T> entity)
    {
      _context.Set<T>().UpdateRange(entity);
    }

  }
}
