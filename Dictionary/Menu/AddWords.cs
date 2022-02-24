using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Menu
{
    class AddWords : ICommand
    {
        //Проверка на английское слово или фразу, которая может содержать пробел
        static bool IsEnglishWord(string word)
        {
            bool isEnglish = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (IsEnglishLetter(word[i]) || word[i] == ' ')
                    isEnglish = true;
                else
                {
                    isEnglish = false;
                    break;
                }
            }
            return isEnglish;
            //Проверка на букву английского алфавита
            static bool IsEnglishLetter(char letter)
            {
                int intLetter = (int)letter;
                if ((letter >= 65 && letter <= 90) || (letter >= 97 && letter <= 122))
                    return true;
                else
                    return false;
            }
        }
        //Проверка на русское слово или фразу, которая может содержать пробел
        static bool IsRussianWord(string word)
        {
            bool isRussian = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (IsRussianLetter(word[i]) || word[i] == ' ')
                    isRussian = true;
                else
                {
                    isRussian = false;
                    break;
                }
            }
            return isRussian;
            //Проверка на букву русского алфавита
            static bool IsRussianLetter(char letter)
            {
                int intLetter = (int)letter;
                if ((letter >= 1040 && letter <= 1103) || letter == 1105 || letter == 1025)
                    return true;
                else
                    return false;
            }
        }
        public void Execute()
        {
            string userInput = Console.ReadLine();
            //Разделим строку на подстроки на основе тире
            string[] splitOverTire = userInput.Split('-');
            //В листы будут добавляться слова, которые пользователь хочет внести в словарь
            List<string> rusWords = new List<string>();
            List<string> engWords = new List<string>();
            List<String> unknownLang = new List<string>();
            //Разделим подстроки на слова или фразы на основе запятой
            if (userInput.Contains(','))
            {
                for (int i = 0; i < splitOverTire.Length; i++)
                {
                    unknownLang.AddRange(splitOverTire[i].Split(','));
                }    
            }
            else
            {
                for (int i = 0; i < splitOverTire.Length; i++)
                {
                    unknownLang.Add(splitOverTire[i]);
                }
            }
            //Определим слова в подходящие по языку листы
            for (int i = 0; i < unknownLang.Count; i++)
            {
                //удалим начальные и конечные пробелы
                unknownLang[i] = unknownLang[i].Trim();
                if (IsEnglishWord(unknownLang[i]))
                    engWords.Add(unknownLang[i]);
                else if (IsRussianWord(unknownLang[i]))
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
                        hasRusWords.Add(new RusWord { Word = rusWords[i] });
                    }
                    else
                    {
                        newRusWords.Add(new RusWord { Word = rusWords[i] });
                    }
                }
                //Определим русские слова из ввода пользователя по определенным листам
                for (int i = 0; i < engWords.Count; i++)
                {
                    //Если слова есть в бд, то они добавляются в лист hasEngWords, если нет, то в лист newRusWords
                    if (db.EngWords.Any(w => w.Word == engWords[i]))
                    {
                        hasEngWords.Add(new EngWord { Word = engWords[i] });
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
                else if (newEngWords.Count > 0)
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
                else if (newRusWords.Count > 0)
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
                else if (hasEngWords.Count > 0 && hasRusWords.Count > 0)
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
            }
        }
    }
}
