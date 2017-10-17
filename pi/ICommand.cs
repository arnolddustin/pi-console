using System;
using System.Linq;

namespace pi
{
    /// <summary>
    /// An instance of a command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// An array of names for the command
        /// </summary>
        /// <returns>An array of strings</returns>
        string[] Name { get; }

        /// <summary>
        /// Description of the purpose of the command
        /// </summary>
        /// <returns>A string</returns>
        string Description { get; }

        /// <summary>
        /// Usage instructions for the command
        /// </summary>
        /// <returns>A string</returns>
        string Usage { get; }

        /// <summary>
        /// Run the command using the specified arguments
        /// </summary>
        /// <param name="args">An array of strings containing the arguments for the command</param>
        void Run(params string[] args);
    }
}