﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Marcus Terminal";
            Console.ForegroundColor = (ConsoleColor)9;
            Core core = new Core();
        }
    }
}
