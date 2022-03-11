using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Commands
{
    class AddWords : ICommand
    {
        public void Execute()
        {
            StopInput.Terms(delegate ()
            {
                //Слова в бд будут находиться в нижнем регистре
                string userInput = StopInput.InputString.ToLower();
                //Разделим строку на подстроки на основе тире
                string[] splitOverTire = userInput.Split('-');
                //В листы будут добавляться слова, которые пользователь хочет внести в словарь
                List<string> rusWords = new List<string>();
                List<string> engWords = new List<string>();
                //В этот лист будут добавлять все слова из консоли
                List<String> unknownLang = new List<string>();
                //В этим листы будут добавлять слова для проверки формата, формат строки должен иметь ввид "word,... - перевод,..." или "слово,... - translate,..."
                List<string> unknownList1 = new List<string>();
                List<string> unknownList2 = new List<string>();
                //Разделим подстроки на слова или фразы на основе запятой
                if (userInput.Contains(','))
                {
                    for (int i = 0; i < splitOverTire.Length; i++)
                    {
                        unknownLang.AddRange(splitOverTire[i].Split(','));
                        //добавим в 1 лист набор слов слева от тире, а во 2 лист набор слов справа от тире
                        if (i == 0)
                            unknownList1.AddRange(splitOverTire[i].Split(','));
                        else if (i == 1)
                            unknownList2.AddRange(splitOverTire[i].Split(','));
                    }
                }
                else
                {
                    for (int i = 0; i < splitOverTire.Length; i++)
                    {
                        unknownLang.Add(splitOverTire[i]);
                        //добавим в 1 лист набор слов слева от тире, а во 2 лист набор слов справа от тире
                        if (i == 0)
                            unknownList1.AddRange(splitOverTire[i].Split(','));
                        else if (i == 1)
                            unknownList2.AddRange(splitOverTire[i].Split(','));
                    }
                }
                //Определим слова в подходящие по языку листы
                //Но сначала узнаем разный ли язык слов из наборов unknownList1 и unknownList2
                if (IsDifferentLanguages(unknownList1, unknownList2))
                {
                    Console.WriteLine("Неверный формат строки");
                    return;
                }
                for (int i = 0; i < unknownLang.Count; i++)
                {
                    //удалим начальные и конечные пробелы
                    unknownLang[i] = unknownLang[i].Trim();
                    if (LanguageCheck.IsEnglishWord(unknownLang[i]))
                        engWords.Add(unknownLang[i]);
                    else if (LanguageCheck.IsRussianWord(unknownLang[i]))
                        rusWords.Add(unknownLang[i]);
                    else throw new Exception("Неизвестный язык или некорректный ввод");
                }
                //Добавим слова в словарь
                using (ApplicationContext db = new ApplicationContext())
                {
                    //В этих листах будут содержаться слова, которых нет в базе данных
                    List<RusWord> newRusWords = new List<RusWord>();
                    List<EngWord> newEngWords = new List<EngWord>();
                    //В этих листах будут содержаться слова, которые уже есть в базе данных
                    List<RusWord> hasRusWords = new List<RusWord>();
                    List<EngWord> hasEngWords = new List<EngWord>();

                    //Определим русские слова из ввода пользователя по определенным листам
                    for (int i = 0; i < rusWords.Count; i++)
                    {
                        //Если слова есть в бд, то они добавляются в лист hasRusWords, если нет, то в лист newRusWords
                        if (db.RusWords.Any(w => w.Word == rusWords[i]))
                        {
                            hasRusWords.Add(db.RusWords.FirstOrDefault(w => w.Word == rusWords[i]));
                        }
                        else
                        {
                            newRusWords.Add(new RusWord { Word = rusWords[i] });
                        }
                    }
                    //Определим английские слова из ввода пользователя по определенным листам
                    for (int i = 0; i < engWords.Count; i++)
                    {
                        //Если слова есть в бд, то они добавляются в лист hasEngWords, если нет, то в лист newRusWords
                        if (db.EngWords.Any(w => w.Word == engWords[i]))
                        {
                            hasEngWords.Add(db.EngWords.FirstOrDefault(w => w.Word == engWords[i]));
                        }
                        else
                        {
                            newEngWords.Add(new EngWord { Word = engWords[i] });
                        }
                    }
                    //Добавим новые слова в бд
                    db.RusWords.AddRange(newRusWords);
                    db.EngWords.AddRange(newEngWords);
                    //Если есть новые слова, то соотнесем перевод слов
                    if (newRusWords.Count > 0 && newEngWords.Count > 0)
                    {
                        for (int i = 0; i < newEngWords.Count; i++)
                        {
                            newEngWords[i].OtherWords.AddRange(newRusWords);
                        }
                    }
                    //Если пользователь добавляет новый перевод английских слов к уже существующим русским словам
                    if (newEngWords.Count > 0)
                    {
                        for (int i = 0; i < newEngWords.Count; i++)
                        {
                            for (int j = 0; j < hasRusWords.Count; j++)
                            {
                                //Получим объект имеющегося русского слова
                                RusWord oldRusWord = db.RusWords.FirstOrDefault(w => w.Word == hasRusWords[j].Word);
                                //Добавим старый русский перевод к новому английскому слову
                                newEngWords[i].OtherWords.Add(oldRusWord);
                            }
                        }
                    }
                    //Если пользователь добавляет новый перевод русский слов к уже существующим английским словам
                    if (newRusWords.Count > 0)
                    {
                        for (int i = 0; i < newRusWords.Count; i++)
                        {
                            for (int j = 0; j < hasEngWords.Count; j++)
                            {
                                //Получим объект имеющегося английского слова
                                EngWord oldEngWord = db.EngWords.FirstOrDefault(w => w.Word == hasEngWords[j].Word);
                                //Добавим старый английский перевод к новому англлийскому слову
                                newRusWords[i].OtherWords.Add(oldEngWord);
                            }
                        }
                    }
                    //Если пользователь соотносит уже имеющиеся слова в базе данных
                    if (hasEngWords.Count > 0 && hasRusWords.Count > 0)
                    {
                        for (int i = 0; i < hasEngWords.Count; i++)
                        {
                            for (int j = 0; j < hasRusWords.Count; j++)
                            {
                                //Получим объект уже имеющегося английского слова
                                EngWord oldEngWord = db.EngWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == hasEngWords[i].Word);
                                //Если у слова нет такого перевода, то добавим его
                                if (!oldEngWord.OtherWords.Any(w => w.Word == hasRusWords[j].Word))
                                {
                                    oldEngWord.OtherWords.Add(hasRusWords[j]);
                                }
                            }
                        }
                    }
                    db.SaveChanges();

                    //добавим/обновим новые или измененные слова в таблицу "забытых" слов
                    List<ForgottenEngWord> forgottenEngWords = new List<ForgottenEngWord>();
                    for (int i = 0; i < hasEngWords.Count; i++)
                    {
                        EngWord engWord = db.EngWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == hasEngWords[i].Word);
                        if (db.ForgottenEngWords.Any(w => w.Word == engWord.Word))
                        {
                            var word = db.ForgottenEngWords.Include(w => w.OtherRusWords).FirstOrDefault(w => w.Word == engWord.Word);
                            word.OtherRusWords = engWord.OtherWords.ToList();
                            word.CountOfRepetitions = 3;
                        }
                        else
                            forgottenEngWords.Add(new ForgottenEngWord() { Word = engWord.Word, OtherRusWords = engWord.OtherWords.ToList(), CountOfRepetitions = 3 });
                    }
                    for (int i = 0; i < newEngWords.Count; i++)
                    {
                        EngWord engWord = db.EngWords.Include(w => w.OtherWords).FirstOrDefault(w => w.Word == newEngWords[i].Word);
                        if (db.ForgottenEngWords.Any(w => w.Word == engWord.Word))
                        {
                            var word = db.ForgottenEngWords.Include(w => w.OtherRusWords).FirstOrDefault(w => w.Word == engWord.Word);
                            word.OtherRusWords = engWord.OtherWords.ToList();
                            word.CountOfRepetitions = 3;
                        }
                        else
                            forgottenEngWords.Add(new ForgottenEngWord() { Word = engWord.Word, OtherRusWords = engWord.OtherWords.ToList(), CountOfRepetitions = 3 });
                    }
                    db.ForgottenEngWords.AddRange(forgottenEngWords);
                    db.SaveChanges();
                }
                bool IsDifferentLanguages(List<string> list1, List<string> list2)
                {
                    List<bool> list1bool = new List<bool>();
                    List<bool> list2bool = new List<bool>();
                    //Добавим в бул списки true или false в зависимости от языка слов элементов list1 и list2
                    for (int i = 0; i < list1.Count; i++)
                    {
                        if (LanguageCheck.IsRussianWord(list1[i]))
                            list1bool.Add(true);
                        else if (LanguageCheck.IsEnglishWord(list1[i]))
                            list1bool.Add(false);
                        else throw new Exception("Неизвестный язык или некорректный ввод");
                    }
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (LanguageCheck.IsRussianWord(list2[i]))
                            list2bool.Add(true);
                        else if (LanguageCheck.IsEnglishWord(list2[i]))
                            list2bool.Add(false);
                        else throw new Exception("Неизвестный язык или некорректный ввод");
                    }
                    //Если в списках есть разные значения, то язык слов в этих списках разный
                    if (list1bool.Contains(true) && list1bool.Contains(false))
                    {
                        return true;
                    }
                    else if (list2bool.Contains(true) && list2bool.Contains(false))
                    {
                        return true;
                    }
                    //Списки не должны быть на одном языке
                    else if (list1bool.Contains(true) && list2bool.Contains(true) || (list1bool.Contains(false) && list2bool.Contains(false)))
                        return true;
                    else
                        return false;
                }
            });
        }
    }
}
