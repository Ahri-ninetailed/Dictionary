using System.Collections.Generic;
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
                    Commands[i].Execute();
                }
            }            
        }
    }
    
}
