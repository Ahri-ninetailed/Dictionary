using System;
using System.Collections.Generic;
namespace Dictionary.Commands
{
    //Класс содержит в себе метод, который выводит случайные числа без повторений от Min включая до Max не включая
    class ExclusiveRandomNumbers
    {
        public List<int> HasAlready { get; set; } = new List<int>();
        public int Min { get; set; }
        public int Max { get; set; }
        private Random random { get; set; } = new Random();
        public ExclusiveRandomNumbers(int min, int max)
        {
            Min = min;
            Max = max;
        }
        public int Next()
        {
            int randomNum = -1;
            while (true)
            {
                randomNum = random.Next(Min, Max);
                if (HasAlready.Contains(randomNum) || HasAlready.Count == Max)
                    continue;
                else
                {
                    HasAlready.Add(randomNum);
                    break;
                }
            }
            return randomNum;
        }
    }
}
