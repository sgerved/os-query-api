using Microsoft.EntityFrameworkCore;
using os_query_api.DataAccess.Models;

namespace os_query_api.DataAccess;

public class ApiEventsDbContext : DbContext
{
    public DbSet<ApiEventModel> ApiEvents { get; set; }
    
    private string DbPath { get; set; }
    
    public ApiEventsDbContext()
    {
        var directory = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(directory);
        DbPath = Path.Combine(path, "api-events.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}