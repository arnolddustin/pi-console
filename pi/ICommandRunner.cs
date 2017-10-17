namespace pi
{
    /// <summary>
    /// Primary interface for running commands
    /// </summary>
    public interface ICommandRunner
    {
        /// <summary>
        /// Find and run a command given using an array of arguments
        /// </summary>
        /// <param name="args">Array of strings containing the arguments. The first element of the array should be the command name to run.</param>
        void RunCommand(params string[] args);
    }
}