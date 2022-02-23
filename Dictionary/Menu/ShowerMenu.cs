using System;
using System.Collections.Generic;
namespace Dictionary.Menu
{
    class ShowerMenu : IShowerMenu
    {
        public List<string> ListCommands { get; set; }
        public ShowerMenu(List<string> listCommands)
        {
            ListCommands = listCommands;
        }

        public void ShowMenu()
        {
            for (int i = 0; i <ListCommands.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ListCommands[i]}");
            }
        }
    }
    
}
