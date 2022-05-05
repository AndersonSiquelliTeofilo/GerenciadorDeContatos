using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
