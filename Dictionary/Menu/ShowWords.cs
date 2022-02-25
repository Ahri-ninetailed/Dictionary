using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Dictionary.Menu
{
    //Вывод имеющихся слов в бд
    class ShowWords : ICommand
    {
        public void Execute()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var words = db.EngWords.Include(w => w.OtherWords).ToList();
                foreach(var engword in words)
                {
                    Console.Write($"{engword.Id}. {engword.Word} - ");
                    for (int i = 0; i < engword.OtherWords.Count; i++)
                    {
                        if (i < engword.OtherWords.Count - 1)
                            Console.Write($"{engword.OtherWords[i].Word}, ");
                        else
                            Console.Write($"{engword.OtherWords[i].Word}.");
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
