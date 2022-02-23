using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Menu
{
    //Демо версия классов команд
    class FirstCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FirstCommand");
    }
    class SecondCommand : ICommand
    {
        public void Execute() => Console.WriteLine("SecondCommand");
    }
    class ThirdCommand : ICommand
    {
        public void Execute() => Console.WriteLine("ThirdCommand");
    }
    class FourthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FourthCommand");
    }
    class FifthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FifthCommand");
    }
    class SixthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("SixthCommand");
    }
    class SeventhCommand : ICommand
    {
        public void Execute() => Console.WriteLine("SeventhCommand");
    }
    class EigthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("EigthCommand");
    }
    class NinthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("NinthCommand"); 
    }
}
