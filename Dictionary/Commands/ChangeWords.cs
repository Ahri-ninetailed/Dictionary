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
                using (ApplicationContext db = new ApplicationContext())
                {
                    var word = db.EngWords.Find(changedWordId);
                    //word будет равен null, если нет слова с таким Id в бд
                    if (!(word is null))
                    {
                        int countWords = db.EngWords.Count();
                        //добавим новое слово
                        new AddWords().Execute();
                        //если слово, добавилось, то удалим старое
                        if (countWords != db.EngWords.Count())
                            db.EngWords.Remove(word);
                        db.SaveChanges();
                    }
                    else
                        Console.WriteLine("Не найдено слово с таким Id");
                }
            }
            else
                Console.WriteLine("Введите Id изменяемого слова или Exit/Enter для выхода");
        }
    }
}
