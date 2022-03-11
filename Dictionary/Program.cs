﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dictionary.Menu;
using Dictionary.Commands;
namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowerMenu showerMenu = new ShowerMenu(
                new List<string>
                {
                    "Слова",
                    "Добавить",
                    "Повторение слов",
                    "Повторение забытых или новых слов",
                    "Удалить",
                    "Изменить",
                    "Найти слово",
                    "Описание программы",
                });
            ChooseCommand chooseCommand = new ChooseCommand(
                new List<ICommand>
                {
                    new ShowWords(),
                    new AddWords(),
                    new RepeatWords(),
                    new RepeatForgottenWords(),
                    new DeleteWords(),
                    new ChangeWords(),
                    new FindWords(),
                    new Description(),
                });
            MenuV01 menu = new MenuV01(showerMenu, chooseCommand);
            while (true)
            {
                menu.ShowerMenu.ShowMenu();
                int num = 0;
                bool isNum = Int32.TryParse(Console.ReadLine(), out num);
                if (!isNum && num == 0 || (num > menu.ChooseCommand.Commands.Count || num < 1))
                {
                    Console.WriteLine("Введите пункт меню");
                    continue;
                }
                else
                {
                    menu.ChooseCommand.Choose(num);
                }
            }
        }
    }
}
