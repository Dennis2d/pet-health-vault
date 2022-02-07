using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemesterHealtVaultProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Models.HealthDocument> healthDocuments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
