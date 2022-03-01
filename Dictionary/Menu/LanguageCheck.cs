using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Menu
{
    //Класс содержит методы, которые проверяют язык передаваемой строчки
    class LanguageCheck
    {
        //Проверка на английское слово или фразу, которая может содержать пробел
        public static bool IsEnglishWord(string word)
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
        public static bool IsRussianWord(string word)
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
    }
}
