using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public partial class ElympicsDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DbSet<RandomNumberRecord> RandomNumberRecords { get; set; }
    
    public ElympicsDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
