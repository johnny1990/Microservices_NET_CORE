using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
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
