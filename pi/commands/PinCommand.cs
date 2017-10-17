using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public abstract class PinCommand
    {
        protected int GetPinNumber(params string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            return number;
        }
    }
}