using Microsoft.EntityFrameworkCore;
using ServiceOrientedArchitecture.Data.Entities;

namespace ServiceOrientedArchitecture.Data.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; } = null!;

    public DbSet<TransactionHistoryEntity> TransactionHistory { get; set; } = null!;
}
