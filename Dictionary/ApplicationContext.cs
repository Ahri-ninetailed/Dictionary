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
            Database.EnsureCreated();
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
        //свойство в котором будет находиться задача, которая получает все английские слова и их перевод из бд
        public static Task<List<EngWord>> GetEngWordsTask { get; set; }
        //асинхронный метод запускает задачу, которая получает все английские слова и их переводы из бд
        async public static Task<List<EngWord>> GetEngWordsAsync()
        {
            List<EngWord> engWords = await Task<List<EngWord>>.Run(() => GetEngWords());
            return engWords;
        }
        //метод получает все английские слова и их переводы из бaд
        public static List<EngWord> GetEngWords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //получим все английские слова и их переводы
                var engWordsList = db.EngWords.Include(w => w.OtherWords).ToList();
                return engWordsList;
            }
        }
    }
}
