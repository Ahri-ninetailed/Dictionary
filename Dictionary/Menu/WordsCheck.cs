using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Dictionary.Menu
{
    //в консоль выводится русский перевод английского слова в случайном порядке, слова которые уже выводились не должны заново выводиться
    //после нажатия клавиши ентер появляется английское слово
    //если пользователь введет в консоль Enter, то появиться следующий перевод. Если не ентер, то это слово добавиться в список не выученных слов

    class WordsCheck : ICommand
    {
        public void Execute()
        {
            Random random = new Random();
            using (ApplicationContext db = new ApplicationContext())
            {
                //получим все английские слова
                var allEngWords = db.EngWords.Include(w => w.OtherWords).ToList();
                //создадим объект класса, который генерируют уникальные случайные числа
                ExclusiveRandomNumbers exclusiveRandomNumbers = new ExclusiveRandomNumbers(0, allEngWords.Count);
                for (int i = 0; i < allEngWords.Count; i++)
                {
                    //получим индекс случайного английского слова
                    int indexEngWord = exclusiveRandomNumbers.Next();
                    //выведем его русский перевод в консоль
                    for (int j = 0; j < allEngWords[indexEngWord].OtherWords.Count; j++)
                    {
                        if (j < allEngWords[indexEngWord].OtherWords.Count - 1)
                            Console.Write($"{allEngWords[indexEngWord].OtherWords[j].Word}, ");
                        else
                            Console.Write($"{allEngWords[indexEngWord].OtherWords[j].Word}.");
                    }
                    Console.WriteLine();
                    //после нажатия клавиши, выведем английское слово
                    Console.ReadKey();
                    Console.WriteLine(allEngWords[indexEngWord].Word);
                    Console.WriteLine();
                }
            }
        }
    }
}
