using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Dictionary.Commands
{
    //Вывод имеющихся слов в бд
    class ShowWords : ICommand
    {
        public void Execute()
        {
            var words = ApplicationContext.GetEngWordsTask.Result;
            for (int j = 0; j < words.Count; j++)
            {
                //Подсчитаем общее кол-во символов в строке, чтоб вывести Id английских слов на одинаковых позициях
                string totalLengthString = "";
                totalLengthString += $"{j + 1}. {words[j].Word} - ";
                Console.Write($"{j + 1}. {words[j].Word} - ");
                for (int i = 0; i < words[j].OtherWords.Count; i++)
                {
                    if (i < words[j].OtherWords.Count - 1)
                    {
                        totalLengthString += $"{words[j].OtherWords[i].Word}, ";
                        Console.Write($"{words[j].OtherWords[i].Word}, ");
                    }
                    else
                    {
                        totalLengthString += $"{words[j].OtherWords[i].Word}.";
                        Console.Write($"{words[j].OtherWords[i].Word}.");
                        //Выведем Id английского слова, 80 отвечает за общее растояние между Id и началом строки
                        //Программа не предусматривает огромное количество переводов к слову, 80 знаков, как мне кажется должно хватить на нужды обычного пользователя словаря
                        try
                        {
                            Console.Write($"{"".PadRight(80 - totalLengthString.Length)}{words[j].Id}");
                        }
                        catch (Exception e)
                        {
                            Console.Write($" {words[j].Id}");
                        }
                    }
                }

                Console.WriteLine();
            }

        }

    }
}
