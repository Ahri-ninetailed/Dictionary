using System;
using System.Collections.Generic;
namespace Dictionary.Menu
{
    /*Абстрактный класс меню, поля которого имеют абстрактные типы IShowerMenu и IChooseCommand
     *В меню будут находиться классы отвечающие за показ меню и выбор команд*/
    abstract class AbstractMenu
    {
        public AbstractMenu()
        {
            
        }
        public AbstractMenu(IShowerMenu showerMenu, IChooseCommand chooseCommand)
        {
            ShowerMenu = showerMenu;
            ChooseCommand = chooseCommand;
        }
        abstract public IShowerMenu ShowerMenu { get; set; }
        abstract public IChooseCommand ChooseCommand { get; set; }
    }
    
}
