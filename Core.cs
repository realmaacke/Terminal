using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace Terminal
{
    public class Core
    {
        string BASEDIRR = @"C:";
        public List<string> cmdList = new List<string>
        {
            // General purpose
            "ls",
            "mkdir",
            "remove",
            "cd",
            "clear",
            "quit",
            "run",
            "disks",
            "help",
            // third party
            "code"
        };

        public Core()
        {
            Directory.SetCurrentDirectory(BASEDIRR);
            init();
            start();
        }

        public void start()
        {
            // gets the current Directory and prints it 
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            Console.Write(currentDir.FullName + ">");

            // user input
            string userInput = Console.ReadLine();

            if (userInput == null)
                refresh();


            string[] input = userInput.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            if (input.Length == 1)
            {
                string function = input[0];

                for(int i = 0; i < cmdList.Count; i++)
                {
                    if(cmdList[i] == function)
                    {
                        exc(function);
                    }
                }
                ThrowError("'" + function + "' Is not recognized as an internal or external command");
            }
            else if (input.Length == 2)
            {
                string function = input[0];
                string param = input[1];
                for (int i = 0; i < cmdList.Count; i++)
                {
                    if (cmdList[i] == function)
                    {
                        execute(function, param);
                    }
                }
            }
            else
            {
                refresh();
            }

            Console.ReadKey();
        }

        void init()
        {
            Console.WriteLine("Marcus Terminal");
            Console.WriteLine("help - for list of commands");
            Console.WriteLine();
        }

        public void exc(string function)
        {
            //Get the method information using the method info class
            MethodInfo mi = this.GetType().GetMethod(function);

            if (mi != null)
            {
                if (mi.ReturnParameter == null)
                    return;


                mi.Invoke(this, null);
            }
            else
            {
                Console.WriteLine("null");
                Console.WriteLine(mi);
            }

        }

        public void execute(string function, string param)
        {
            //Get the method information using the method info class
            MethodInfo mi = this.GetType().GetMethod(function);

            if (mi != null)
            {
                mi.Invoke(this, new object[] { param });
            }
            else
            {
                Console.WriteLine("null");
            }
        }

        public void help()
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "ls",      "[no params]",      "- Lists information about the current Directory"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "mkdir",   "[name]",           "- Creates folder if it dosen't exists"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "remove",  "[file]",           "- Creates folder if it dosen't exists"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "cd",      "[params]",         "- Navigates through the Directory"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "clear",   "[no params]",      "- Clears the screen"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "quit",    "[no params]",      "- Quits the aplication"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "run",     "[File]",           "- Run specified File with default execute behavior"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "disks",   "[no params]",      "- Lists information about the machines Disk Drives"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "code",    "[args]",           "- Opens VSCode with custom args"));
            Console.WriteLine(String.Format("{0,-6}{1,-12}{2,12}\n", "help",    "[no params]",      "- Lists all commands"));
            refresh();
        }

        public void ls()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            Console.WriteLine();
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine("{0, -40}\t directory", d.Name);
            }

            foreach (FileInfo f in dir.GetFiles())
            {
                Console.WriteLine("{0, -40}\t File", f.Name);
            }
            refresh();
        }

        public void disks()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach(DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                    decimal available = d.AvailableFreeSpace;
                    decimal TotalFreeSpace = d.TotalFreeSpace;
                    decimal TotalSize = d.TotalSize;

                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} GB",
                       Math.Ceiling(available));

                    Console.WriteLine(
                        "  Total available space:          {0, 15} GB",
                         Math.Ceiling(TotalFreeSpace));

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} GB ",
                         Math.Ceiling(TotalSize));
                }
            }

            refresh();
        }


        public void refresh()
        {
            Console.WriteLine();
            start();
        }

        public void clear()
        {
            Console.Clear();
            start();
        }

        public void cd(string parameter)
        {

            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameter));

            if (Directory.Exists(newPath))
            {
                Directory.SetCurrentDirectory(newPath);

                refresh();
            }
            else
            {
                ThrowError("Invalid Path");
            }
        }


        public void mkdir(string parameters)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameters));

            if (Directory.Exists(newPath))
            {
                ThrowError("Directory Already Exists");
                refresh();
            }
            else
            {
                Directory.CreateDirectory(newPath);
                refresh();
            }
        }

        public void remove(string parameters)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameters));

            if (!Directory.Exists(newPath))
            {
                ThrowError("Directory Already Exists");
                refresh();
            }
            else
            {
                Directory.Delete(newPath);
                refresh();
            }
        }


        public void run(string parameter)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath (Path.Combine(path, parameter));

            Process.Start(newPath, parameter);
            refresh();
        }

        public void quit()
        {
            Environment.Exit(0);
        }

        public void ThrowError(string error = null)
        {

            Console.WriteLine();
            Console.WriteLine(error);
            refresh();

        }


        //third party
        public void code(string parameter)
        {
            Process.Start("\"C:\\Users\\Marcus-PC\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe\"", parameter);

            refresh();
        }
    }
}
