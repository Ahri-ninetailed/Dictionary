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
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Dictionary.db");
        }
    }
}
