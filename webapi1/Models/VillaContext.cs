using Microsoft.EntityFrameworkCore;
using webapi1.Models;
namespace webapi1.Models
{



    public class VillaContext : DbContext
    {
        public VillaContext(DbContextOptions<VillaContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<VillaBarChart> VillaBarCharts { get; set; }
    }

}