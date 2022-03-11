using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Dictionary.Commands
{
    //в консоль выводится русский перевод английского слова в случайном порядке, слова которые уже выводились не должны заново выводиться
    //после нажатия клавиши ентер появляется английское слово
    //если пользователь введет в консоль Enter, то появиться следующий перевод. Если не ентер, то это слово добавиться в список не выученных слов
    class RepeatWords : ICommand
    {
        public void Execute()
        {
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
                    Console.ReadKey(true);
                    Console.WriteLine(allEngWords[indexEngWord].Word);
                    string strOutput = Console.ReadLine();
                    //если пользователь введет exit в любом регистре, то завершится повторение слов
                    if (strOutput.ToLower() == "exit")
                        break;
                    //Если пользователь введет не Enter, то добавим это слово в таблицу забытых слов
                    if (strOutput != "")
                    {
                        //если в таблице забытых слов еще нет этого слова, то добавим его
                        if (!db.ForgottenEngWords.Any(w => w.Word == allEngWords[indexEngWord].Word))
                            db.ForgottenEngWords.Add(new ForgottenEngWord() { Word = allEngWords[indexEngWord].Word, OtherRusWords = allEngWords[indexEngWord].OtherWords.ToList() });
                        else
                        {
                            //если в таблице забытых слов уже есть это слово, то обновим его счетчик
                            db.ForgottenEngWords.FirstOrDefault(w => w.Word == allEngWords[indexEngWord].Word).CountOfRepetitions = 3;
                        }
                        db.SaveChanges();
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
