﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Dictionary.Commands
{
    class FifthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("FifthCommand");
    }
    class NinthCommand : ICommand
    {
        public void Execute() => Console.WriteLine("NinthCommand");
    }
}