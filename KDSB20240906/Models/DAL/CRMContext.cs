using KDSB20240906.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace KDSB20240906.Models.DAL
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options)
       : base(options)
        {
        }

        public DbSet<ProductKDSB> products { get; set; }
    }
}
