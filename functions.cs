using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terminal;

namespace Terminal
{
    public class functions : Commands
    {
        #region execute methods
        public void exc(string function)
        {
            //Get the method information using the method info class
            MethodInfo mi = GetType().GetMethod(function);

            if (mi != null)
            {
                if (mi.ReturnParameter == null)
                    return;


                mi.Invoke(this, null);
            }
            else
            {
                Console.WriteLine("'" + function + "' Is not recognized as an internal of external command");
            }

        }

        public void execute(string function, string[] param)
        {
            //Get the method information using the method info class
            MethodInfo mi = this.GetType().GetMethod(function);

            if (mi != null)
            {
                mi.Invoke(this, new object[] { param });
            }
            else
            {
                Console.WriteLine("'" + function + "' Is not recognized as an internal of external command");
            }
        }
        #endregion

        #region Commands

        public void cd(string[] parameter)
        {

            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameter[0]));

            if (Directory.Exists(newPath))
            {
                Directory.SetCurrentDirectory(newPath);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Path");
            }
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
        }

        public void disks()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
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
        }

        public void run(string[] parameter)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameter[0]));

            Process.Start(newPath, parameter[0]);
        }

        public void mkdir(string[] parameters)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameters[0]));

            if (Directory.Exists(newPath))
            {
                Console.WriteLine("Directory Already Exists");
            }
            else
            {
                Directory.CreateDirectory(newPath);
            }
        }
        public void remove(string[] parameters)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath;

            newPath = Path.GetFullPath(Path.Combine(path, parameters[0]));

            if (!Directory.Exists(newPath))
            {
                Console.WriteLine("Directory Already Exists");
            }
            else
            {
                Directory.Delete(newPath);
            }
        }

        public void code(string[] parameter)
        {
            Process.Start("\"C:\\Users\\Marcus-PC\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe\"", parameter[0]);
        }


        #endregion

        #region utillities
        public void help()
        {
            Console.WriteLine();

            Console.WriteLine(String.Format("{0,-12}{1,-12}{2,12}\n", "Name", "args", "Description"));


            foreach (var d in CommandList)
            {
                Console.WriteLine(String.Format("{0,-12}{1,-12}{2,12}\n", d.Name, d.argsDescription, d.Description));
            }
        }
        public void quit()
        {
            Environment.Exit(0);
        }
        public void clear()
        {
            Console.Clear();
        }
        #endregion
    }
}
