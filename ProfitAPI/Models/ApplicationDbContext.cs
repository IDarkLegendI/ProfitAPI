using Microsoft.EntityFrameworkCore;
namespace ProfitAPI.Models;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        try
        { 
            this.Database.OpenConnection();
            Console.WriteLine("Successfully connected through ApplicationDbContext.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ApplicationDbContext connection error: {ex.Message}");
        }
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                "Server=mysql;Database=products;User=root;Password=example;",
                ServerVersion.AutoDetect("Server=mysql;Database=products;User=root;Password=example;"),
                options =>
                {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
        }
    }


    public DbSet<LocalProduct> Products { get; set; }
}
