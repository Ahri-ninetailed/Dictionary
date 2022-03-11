using System.Collections.Generic;
using Dictionary.Commands;
using System;
namespace Dictionary.Menu
{
    class ChooseCommand : IChooseCommand
    {
        public ChooseCommand(List<ICommand> commands)
        {
            Commands = commands;
        }
        public List<ICommand> Commands { get; set; }
        public void Choose(int num)
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                if (num == i + 1)
                {
                    if (Commands[i] is AddWords ||
                        Commands[i] is DeleteWords ||
                        Commands[i] is FindWords ||
                        Commands[i] is ChangeWords)
                        StopInput.Terms(new Action(Commands[i].Execute));
                    else
                        Commands[i].Execute();
                }
            }
        }
    }
    
}
