using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Commands
{
    class RepeatForgottenWords : ICommand
    {
        public void Execute()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //получим все забытые слова
                var allForgottenWords = db.ForgottenEngWords.Include(w => w.OtherRusWords).ToList();
                //создадим объект класса, который генерируют уникальные случайные числа
                ExclusiveRandomNumbers exclusiveRandomNumbers = new ExclusiveRandomNumbers(0, allForgottenWords.Count);
                for (int i = 0; i < allForgottenWords.Count; i++)
                {
                    //получим индекс случайного английского слова
                    int indexForgottenWord = exclusiveRandomNumbers.Next();
                    //выведем его русский перевод в консоль
                    for (int j = 0; j < allForgottenWords[indexForgottenWord].OtherRusWords.Count; j++)
                    {
                        if (j < allForgottenWords[indexForgottenWord].OtherRusWords.Count - 1)
                            Console.Write($"{allForgottenWords[indexForgottenWord].OtherRusWords[j].Word}, ");
                        else
                            Console.Write($"{allForgottenWords[indexForgottenWord].OtherRusWords[j].Word}.");
                    }
                    Console.WriteLine();
                    //после нажатия клавиши, выведем английское слово
                    Console.ReadKey(true);
                    Console.WriteLine(allForgottenWords[indexForgottenWord].Word);
                    string strOutput = Console.ReadLine();
                    //Если пользователь введет не Enter, то обновим счетчик у этого слова
                    if (strOutput != "")
                    {
                        allForgottenWords[indexForgottenWord].CountOfRepetitions = 3;
                    }
                    else
                    {
                        allForgottenWords[indexForgottenWord].CountOfRepetitions -= 1;
                    }
                    Console.WriteLine();
                    try
                    {
                        db.SaveChanges();
                    }
                    catch { }
                }
            }
        }
    }
}
