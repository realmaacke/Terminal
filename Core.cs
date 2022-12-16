using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Terminal
{
    public class Core : functions
    {
        private readonly string BASEDIRR = @"C:";
        private string VERSION = "1.0.0";
        public Core()
        {
            _version = VERSION;


            Directory.SetCurrentDirectory(BASEDIRR);
            init();
            start();
        }

        public void start()
        {
            // gets the current Directory and prints it 
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            Console.Write(currentDir.FullName + ">");

            string userInput = Console.ReadLine();

            if (userInput == null)
                refresh();

            string[] input = userInput.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            // check for empty
            if (input.Length <= 0)
                refresh();


            string method = input[0];

            foreach (var cmd in CommandList)
            {
                if (method == cmd.Name)
                {

                    if (cmd.hasArgs && input.Length > 1)
                    {
                        List<string> args = new List<string>();

                        // iterates input array, skips 0 index and adding to list
                        for (int i = 1; i < input.Length; i++)
                        {
                            args.Add(input[i]);
                        }

                        execute(method, args.ToArray());
                        refresh();

                    }
                    else
                    {
                        if (input.Length == 1)
                        {
                            exc(method);
                            refresh();
                        }
                        else
                        {
                            ThrowError("The syntax of the program is incorrect");
                            refresh();
                        }
                    }
                }
            }

            ThrowError("'" + method + "' Is not recognized as an internal of external command");
            refresh();
        }

        void init()
        {
            Console.WriteLine("Marcus Terminal");
            Console.WriteLine("help - for list of commands");
            Console.WriteLine();
        }
        public void refresh()
        {
            Console.WriteLine();
            start();
        }

        public void ThrowError(string error = null)
        {

            Console.WriteLine();
            Console.WriteLine(error);
            refresh();
        }
    }
}
