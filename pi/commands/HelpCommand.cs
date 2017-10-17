using System.Collections.Generic;
using System;

namespace pi.commands
{
    public class HelpCommand : ICommand
    {
        List<ICommand> _otherCommands;

        public string[] Name { get { return new string[] { "help", "?" }; } }
        public string Description { get { return "Displays a list of commands"; } }
        public string Usage { get { return ""; } }

        public HelpCommand(IEnumerable<ICommand> othercommands)
        {
            _otherCommands = new List<ICommand>(othercommands);
        }

        public void Run(params string[] args)
        {
            Console.WriteLine("Available commands:\n");

            foreach (var cmd in _otherCommands)
            {
                Console.WriteLine("  {0}:\n\t{1}", string.Join(" | ", cmd.Name), cmd.Description);

                if (cmd.Usage.Length > 0)
                    Console.WriteLine("\tex: {0}", cmd.Usage);
            }
        }
    }
}