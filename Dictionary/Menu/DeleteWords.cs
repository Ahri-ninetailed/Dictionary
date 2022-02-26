using System;
namespace Dictionary.Menu
{
    class DeleteWords : ICommand
    {
        public void Execute()
        {
            string outputString = null;
            //Пользователь может удалять слова пока не введет exit или Enter
            while (outputString != "exit" || outputString != "")
            {
                outputString = Console.ReadLine();
                //Если пользователь ввел в консоль exit в любом регистре или нажал Enter - выйти из метода удаления
                if (outputString.ToLower() == "exit" || outputString == "")
                {
                    return;
                }
                bool isNum = Int32.TryParse(outputString, out int deletedWordId);
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
}
