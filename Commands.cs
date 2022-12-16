using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    public class Commands
    {
        public List<Cmds> CommandList = new List<Cmds>()
        {
            new Cmds
            {
                Name = "ls",
                Description = "Lists information about the current Directory",
                hasArgs = false,
                argsDescription = "[no args]"
            },
            new Cmds
            {
                Name = "mkdir",
                Description = "Creates folder if it dosen't exists",
                hasArgs = true,
                argsDescription = "[name]"
            },
            new Cmds
            {
                Name = "remove",
                Description = "Remove specified File",
                hasArgs = true,
                argsDescription = "[file]"
            },
            new Cmds
            {
                Name = "cd",
                Description = "Navigates through the Directory",
                hasArgs = true,
                argsDescription = "[args]"
            },

            new Cmds
            {
                Name = "clear",
                Description = "Clears the screen",
                hasArgs = false,
                argsDescription = "[no args]"
            },

            new Cmds
            {
                Name = "quit",
                Description = "Quits the aplication",
                hasArgs = false,
                argsDescription = "[no args]"
            },

            new Cmds
            {
                Name = "run",
                Description = "Run specified File with default execute behavior",
                hasArgs = true,
                argsDescription = "[File]"
            },
            new Cmds
            {
                Name = "disks",
                Description = "Lists information about the machines Disk Drives",
                hasArgs = false,
                argsDescription = "[no args]"
            },

            new Cmds
            {
                Name = "help",
                Description = "Lists all commands",
                hasArgs = false,
                argsDescription = "[no args]"
            },
            new Cmds
            {
                Name = "code",
                Description = "Opens VSCode with custom args",
                hasArgs = true,
                argsDescription = "[args]"

            },

            new Cmds
            {
                Name = "version",
                Description = "Opens VSCode with custom args",
                hasArgs = false,
                argsDescription = "[no args]"

            },
        };

        public struct Cmds
        {
            public string Name;
            public string Description;
            public bool hasArgs;
            public string argsDescription;
        }
    }
}
