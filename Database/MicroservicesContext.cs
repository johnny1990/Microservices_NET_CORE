using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class MicroservicesContext : DbContext
    {
        public MicroservicesContext(DbContextOptions<MicroservicesContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }

    }
}
