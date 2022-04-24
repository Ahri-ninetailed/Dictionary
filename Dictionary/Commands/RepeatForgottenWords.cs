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

                //выведем количество забытых слов в консоль
                Console.WriteLine(allForgottenWords.Count);

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
                    //после 40 символа в строку добавится количество повторений забытого слова
                    Console.WriteLine($"{allForgottenWords[indexForgottenWord].Word.PadRight(40)}{allForgottenWords[indexForgottenWord].CountOfRepetitions}");
                    string strOutput = Console.ReadLine();
                    //если пользователь введет exit в любом регистре, то прекратится повторение слов
                    if (strOutput.ToLower() == "exit")
                        break;
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
