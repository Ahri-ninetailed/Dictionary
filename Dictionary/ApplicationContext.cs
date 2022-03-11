using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Dictionary
{
    //класс контекста данных
    public class ApplicationContext : DbContext
    {
        public DbSet<RusWord> RusWords => Set<RusWord>();
        public DbSet<EngWord> EngWords => Set<EngWord>();
        public DbSet<ForgottenEngWord> ForgottenEngWords => Set<ForgottenEngWord>();
        public ApplicationContext() 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Dictionary.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForgottenEngWord>().Property(u => u.CountOfRepetitions).HasDefaultValue(3);
            modelBuilder.Entity<ForgottenEngWord>().HasCheckConstraint("CountOfRepetitions", "CountOfRepetitions >= 0 AND CountOfRepetitions <= 3");
        }
    }
}
