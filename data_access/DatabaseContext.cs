using domain_layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class DatabaseContext : IdentityDbContext<SystemUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genero>(e => e.ToTable("Genero"));
            modelBuilder.Entity<Persona>(e => e.ToTable("Persona"));
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }

    }
}
