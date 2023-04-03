using System.Linq.Expressions;
using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly BoutiqueAppAPIDbContext _context;

    public ReadRepository(BoutiqueAppAPIDbContext context)
    {
        _context = context;
    }
    //Tracling mekanizmasini performans icin istersen ekleyebilirsin.
    public DbSet<T> Table => _context.Set<T>();
    
    public IQueryable<T> GetAll()
    {
        var query = Table.AsQueryable();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
    {
        var query = Table.Where(method);
        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
    {
        var query = Table.AsQueryable();
        return await query.FirstOrDefaultAsync(method);//Testten geceni alip getirir.
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var query = Table.AsQueryable();
        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }
}