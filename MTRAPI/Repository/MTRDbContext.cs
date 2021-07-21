using Microsoft.EntityFrameworkCore;
using MTR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Repository
{
    public class MTRDbContext:DbContext
    {
        public MTRDbContext(DbContextOptions<MTRDbContext> opt) :base(opt)
        {

        }

        public DbSet<TaxPayer> TaxPayers { get; set; }
        public DbSet<ETR> ETRs { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ETRReceipt> ETRReceipts { get; set; }


    }
}
