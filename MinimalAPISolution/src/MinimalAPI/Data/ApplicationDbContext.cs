using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data.Entities;

namespace MinimalAPI.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Todo> Todo { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}