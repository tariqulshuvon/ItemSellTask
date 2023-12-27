using ItemSellTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemSellTask.DbCon;

public class DbConContext : DbContext
{
    public DbConContext(DbContextOptions<DbConContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
}
