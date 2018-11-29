using Company_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        { }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company
                {   
                    CompanyId = 1,
                    Name = "First Company",
                    Address1 = "Scotland Yard",
                    Address2 = "221B BAKER STREET LONDON",
                    LastClickOn = new DateTime(2018, 11, 17)
                },
                new Company
                {
                    CompanyId = 2,
                    Name = "Second Company",
                    Address1 = "Walse Yard",
                    Address2 = "221B BAKER Avenue LONDON",
                    LastClickOn = new DateTime(2018, 11, 17)
                }
            );
        }
    }
}
