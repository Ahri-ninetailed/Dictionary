using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Commands
{
    //изменяться слова фактически не будут, сначала будут добавляться новые слова, а потом удаляться старые, если новые были добавленые
    class ChangeWords : ICommand
    {
        public void Execute()
        {
            string input = Console.ReadLine().ToLower();
            StopInput.InputString = input;
            if (input == "" || input == "exit")
                return;
            bool isNum = Int32.TryParse(input, out int changedWordId);
            //если пользователь ввел число, то попробовать удалить слово
            if (isNum)
            {
                //Этой переменной будет присваиваться слово, которое мы найдем по Id
                EngWord word;
                using (ApplicationContext db = new ApplicationContext())
                {
                    //найдем слово по id
                    word = db.EngWords.Find(changedWordId);
                }
                if (word is null)
                    Console.WriteLine("Не найдено слово с таким Id");
                else
                {
                    //В эту переменную будем записывать количество английских слов
                    int countWords;

                    //если нашлось, то подгрузим перевод этого слова
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        word = db.EngWords.Where(w => w.Id == word.Id).Include(w => w.OtherWords).FirstOrDefault(w => w.Word == word.Word);

                        countWords = db.EngWords.Count();
                        //удалим слово
                        db.EngWords.Remove(word);
                        db.SaveChanges();
                    }
                   
                    //добавим новое слово
                    new AddWords().Execute();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        //если слово, не добавилось, то добавим заново удаленное слово
                        if (countWords != db.EngWords.Count())
                        {
                            db.EngWords.Add(new EngWord() { Word = word.Word, OtherWords = word.OtherWords });
                        }
                        else
                        {
                            //также удалим слово из таблицы забытых слов
                            var forgottenWord = db.ForgottenEngWords.Include(w => w.OtherRusWords).FirstOrDefault(w => w.Word == word.Word);
                            if (!(forgottenWord is null))
                                db.ForgottenEngWords.Remove(forgottenWord);
                        }
                        db.SaveChanges();
                    }
                    
                    //после изменения слов, запустим поток, который получает слова для вывода
                    ApplicationContext.GetEngWordsTask = ApplicationContext.GetEngWordsAsync();
                }
            }
            else
                Console.WriteLine("Введите Id изменяемого слова или Exit/Enter для выхода");
        }
    }
}
