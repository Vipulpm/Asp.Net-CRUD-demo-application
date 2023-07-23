using LEDGER_BOOK.Models;
using Microsoft.EntityFrameworkCore;

namespace LEDGER_BOOK.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<CreditStatement> Credits { get; set; }
        public DbSet<DebitStatement> Debits { get; set; }
    }
}
