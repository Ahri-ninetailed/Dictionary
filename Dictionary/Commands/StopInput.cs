using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Commands
{
    class StopInput
    {
        public static string InputString { get; set; }
        public static void Terms(Action method)
        {
            InputString = null;
            //Пользователь может может взаимодействовать с консолью пока не введет exit или Enter
            while (InputString != "exit" || InputString != "")
            {
                //Если пользователь ввел в консоль exit в любом регистре или нажал Enter - выйти из метода удаления
                if (InputString?.ToLower() == "exit" || InputString == "")
                {
                    return;
                }
                method();
            }
        }
    }
}
