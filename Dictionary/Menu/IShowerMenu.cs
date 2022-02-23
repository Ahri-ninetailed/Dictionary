using System.Collections.Generic;
namespace Dictionary.Menu
{
    /*Классы, которые реализуют этот интерфейс должны содержать лист строк с названиями команд и метод отображающий меню
    */
    interface IShowerMenu
    {
        abstract public List<string> ListCommands { get; set; }
        void ShowMenu();
    }
    
}
