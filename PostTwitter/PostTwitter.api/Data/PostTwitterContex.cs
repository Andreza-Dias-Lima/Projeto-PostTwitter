using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostTwitter.api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace PostTwitter.api.Data
{
    public class PostTwitterContext : DbContext
    {
        public PostTwitterContext(DbContextOptions<PostTwitterContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Post>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Post>().HasOne(p => p.Usuario);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
