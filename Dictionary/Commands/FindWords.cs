using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Commands
{
    class FindWords : ICommand
    {
        public void Execute()
        {

            string inputString = Console.ReadLine().Trim().ToLower();
            StopInput.InputString = inputString;
            if (inputString == "exit" || inputString == "")
                return;
            using (ApplicationContext db = new ApplicationContext())
            {
                EngWord foundEngWord = db.EngWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == inputString);
                RusWord foundRusWord = db.RusWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == inputString);
                if (!(foundEngWord is null))
                {
                    Console.Write($"{foundEngWord.Word} - ");
                    for (int i = 0; i < foundEngWord.OtherWords.Count; i++)
                    {
                        if (i < foundEngWord.OtherWords.Count - 1)
                            Console.Write($"{foundEngWord.OtherWords[i].Word}, ");
                        else
                            Console.Write($"{foundEngWord.OtherWords[i].Word}.     {foundEngWord.Id}");
                    }
                    Console.WriteLine();
                }
                else if (!(foundRusWord is null))
                {
                    for (int i = 0; i < foundRusWord.OtherWords.Count; i++)
                    {
                        EngWord engWord = db.EngWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == foundRusWord.OtherWords[i].Word);
                        Console.Write($"{engWord.Word} - ");
                        for (int j = 0; j < engWord.OtherWords.Count; j++)
                        {
                            if (j < engWord.OtherWords.Count - 1)
                                Console.Write($"{engWord.OtherWords[j].Word}, ");
                            else
                                Console.Write($"{engWord.OtherWords[j].Word}.     {engWord.Id}");
                        }
                        Console.WriteLine();
                    }
                }
                else if ((foundEngWord is null) && (foundRusWord is null))
                {
                    Console.WriteLine("Не найдено такое слово");
                }
            }

        }
    }
}
