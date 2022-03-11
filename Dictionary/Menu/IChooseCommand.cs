﻿using System.Collections.Generic;
using Dictionary.Commands;
namespace Dictionary.Menu
{
    /*Реализующий класс должен содержать метод выбора команды и лист команд*/
    interface IChooseCommand
    {
        public List<ICommand> Commands { get; set; }
        public void Choose(int num);
    }
    
}
