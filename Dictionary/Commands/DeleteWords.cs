using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Dictionary.Commands
{
    class DeleteWords : ICommand
    {
        public void Execute()
        {
            string output = Console.ReadLine().ToLower();
            StopInput.InputString = output;
            if (output == "exit" || output == "")
                return;
            bool isNum = Int32.TryParse(output, out int deletedWordId);
            //если пользователь ввел число, то попробовать удалить слово
            if (isNum)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var word = db.EngWords.Find(deletedWordId);
                    //word будет равен null, если нет слова с таким Id в бд
                    if (!(word is null))
                    {
                        db.EngWords.Remove(word);

                        //также удалим слово из таблицы забытых слов
                        var forgottenWord = db.ForgottenEngWords.FirstOrDefault(w => w.Word == word.Word);
                        if (!(forgottenWord is null))
                            db.ForgottenEngWords.Remove(forgottenWord);
                        db.SaveChanges();

                        //после удаления слов, запустим поток, который получает слова для вывода
                        ApplicationContext.GetEngWordsTask = ApplicationContext.GetEngWordsAsync();
                    }
                    else
                        Console.WriteLine("Не найдено слово с таким Id");
                }
            }
            else
                Console.WriteLine("Введите число или Exit/Enter для выхода");
        }
    }
}
