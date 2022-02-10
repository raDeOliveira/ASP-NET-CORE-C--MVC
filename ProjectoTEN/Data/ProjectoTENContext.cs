#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProjectoTEN.Models;


namespace ProjectoTEN.Data {
    public class ProjectoTENContext : DbContext {
        public ProjectoTENContext(DbContextOptions<ProjectoTENContext> options)
            : base(options) {
            
        }

        public ProjectoTENContext() {
        }
        

        public DbSet<Criticas> Criticas { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<LinkVideo> LinksVideos { get; set; }
        
        
    }
}
