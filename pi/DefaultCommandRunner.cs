using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using pi.commands;

namespace pi
{
    /// <summary>
    /// Default implementation of the ICommandRunner interface
    /// </summary>
    public class DefaultCommandRunner : ICommandRunner
    {
        /// <summary>
        /// List of commands supported by this ICommandRunner
        /// </summary>
        List<ICommand> _commands;

        /// <summary>
        /// Constructs a DefaultCommandRunner
        /// </summary>
        /// <param name="commands">IEnumerable of ICommand instances</param>
        public DefaultCommandRunner(IEnumerable<ICommand> commands)
        {
            _commands = new List<ICommand>(commands);
        }

        #region ICommandRunner Interface

        public void RunCommand(params string[] args) => GetCommand(args).Run(args);

        #endregion

        /// <summary>
        /// Finds a command from the list of commands with a name that matches the first argument
        /// </summary>
        /// <param name="args">Array of strings containing the arguments.</param>
        /// <returns>The specified command, or HelpCommand if not found</returns>
        ICommand GetCommand(params string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No command specified.\n");
                return new HelpCommand(_commands);
            }

            try
            {
                var cmd = _commands.Single(c => c.Name.Contains(args[0].ToLower()));

                var cc = Console.ForegroundColor;
                Console.Write("Valid command: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(args[0].ToLower());
                Console.ForegroundColor = cc;
                Console.WriteLine("\n");

                return cmd;
            }
            catch (InvalidOperationException)
            {
                var c = Console.ForegroundColor;
                Console.Write("Invalid command: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0}\n\n", args[0].ToLower());
                Console.ForegroundColor = c;

                return new HelpCommand(_commands);
            }
        }
    }
}