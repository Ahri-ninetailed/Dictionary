﻿using System;
namespace Dictionary.Commands
{
    class Description : ICommand
    {
        public void Execute() => Console.WriteLine(@"  Программа представляет из себя словарь, в который можно записывать русские и английские слова
    
    Команда ""Слова"": отображает список всех слов в базе данных, цифры справа от слов это их Id, с помощью которого со словами можно выполнять различные действия
    
    Команда ""Добавить"": добавляет новые слова в словарь, формат строки может быть таким ""english word, englishword - русское слово, русское слово"" или наоборот, пробелы рядом с тире необязательны. Если вы забыли дописать какое-то слово, то с помощью этой команды вы можете это сделать. Все новые слова добавляются в таблицу забытых слов
    
    Команда ""Повторение слов"": выводит в случайном порядке перевод английских слов, причем если слово уже выводилось, то оно больше не будет выводится. Дальше пользователь должен нажать на любую клавишу клавиатуры, после этого ему покажется английское слово, если пользователь после этого введет в консоль не ""Enter"", то слово будет добавлено в таблицу забытых слов, если пользователь введет ""Exit"" в любом регистре, то повторение слов закончится
    
    Команда ""Повторение забытых или новых слов"": отображает перевод всех английских забытых слов в случайном порядке, слова, которые уже были показываться не будут. После перевода нужно нажать любую клавишу, чтоб посмотреть английское слово. Если пользователь после этого введет, не ""Enter"", то счетчик слова обновится, если введет ""Enter"", то счетчик слова уменьшится на 1, чтоб слово пропало из таблицы забытых слов, нужно правильно вспомнить его перевод 3 раза. При вводе ""Exit"" в любом регистре, повторение забытых или новых слов закончится
    
    Команда ""Изменить"": чтоб изменить слово, нужно ввести Id слова, которое вы хотите изменить, после ввода Id, нужно ввести слово и его перевод, как если бы это была команда ""Добавить"". Можно изменять слова до тех пор пока вы не нажмете ""Enter"" или не введете ""Exit"" в любом регистре
    
    Команда ""Найти слово"": введите слово на любом языке, программа выведет все совпадения в базе данных, можно вводить слова пока не нажмете ""Enter"" или не введете ""Exit"" в любом регистре");
    }
}