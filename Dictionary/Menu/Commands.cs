﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Menu
{
    class FourthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FourthCommand");
    }
    class FifthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FifthCommand");
    }
    class SeventhCommand : ICommand
    {
        public void Execute() => Console.WriteLine("SeventhCommand");
    }
    class NinthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("NinthCommand");
    }
}