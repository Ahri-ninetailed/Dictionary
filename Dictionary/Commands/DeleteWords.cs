using System;
namespace Dictionary.Commands
{
    class DeleteWords : ICommand
    {
        public void Execute()
        {
            bool isNum = Int32.TryParse(StopInput.InputString, out int deletedWordId);
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
                        db.SaveChanges();
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
