using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Persistance.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
