using AcmeCorpAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpAPI.DBContext
{
    public class AcmeCorpDBContext : DbContext
    {
        public AcmeCorpDBContext(DbContextOptions<AcmeCorpDBContext> options)
        : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
