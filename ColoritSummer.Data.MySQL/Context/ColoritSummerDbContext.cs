using ColoritSummer.Data.MySQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ColoritSummer.Data.MySQL.Context
{
    public class ColoritSummerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCard> ProductCards { get; set; }

        public ColoritSummerDbContext(DbContextOptions<ColoritSummerDbContext> optionsBuilder)
            : base(optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
