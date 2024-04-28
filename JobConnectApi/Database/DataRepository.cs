using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Database;

public class DataRepository<T> : IDataRepository<T> where T : class
{
    
    private readonly DatabaseContext _db;
    private readonly DbSet<T> table;

    public DataRepository(DatabaseContext db)
    {
        _db = db;
        table = _db.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await table.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await table.FindAsync(id) ?? throw new KeyNotFoundException();
    }

    public async Task AddAsync(T entity)
    {
        await table.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        table.Remove(entity);
        return Task.CompletedTask;
    }
    

    public async Task<bool> Save()
    {
        return await _db.SaveChangesAsync() > 0;
    }
}