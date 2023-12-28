using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public interface INumbersRepository
{
    Task<RandomNumberRecord> GetByIdAsync(Guid id);
    Task<List<RandomNumberRecord>> ListAsync(int skip=0, int take=0);
    Task<RandomNumberRecord> AddAsync(RandomNumberRecord randomNumberRecord);
}

public class BasicNumbersRepository : INumbersRepository
{
    private readonly ElympicsDbContext _dbContext;
    
    public BasicNumbersRepository(ElympicsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RandomNumberRecord> GetByIdAsync(Guid id)
    {
        var record = await _dbContext.RandomNumberRecords.FindAsync(id);
        
        if (record == null)
        {
            throw new Exception($"Entity with id {id} not found");
        }
        return record;
    }

    public async Task<List<RandomNumberRecord>> ListAsync(int skip = 0, int take = 0)
    {
       var records = await _dbContext.RandomNumberRecords.Skip(skip).Take(take).ToListAsync();
       return records;
    }

    public async Task<RandomNumberRecord> AddAsync(RandomNumberRecord randomNumberRecord)
    {
        var record = await _dbContext.RandomNumberRecords.AddAsync(randomNumberRecord);
        await _dbContext.SaveChangesAsync();
        return record.Entity;
    }
}