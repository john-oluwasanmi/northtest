using Microsoft.EntityFrameworkCore;
using Nhs.Test.Api.Model;

namespace Nhs.Test.Api
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                
        }
    }
}