using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class OutputCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "output" }; } }
        public string Description { get { return "Sets a pin to output"; } }
        public string Usage { get { return "output <<pin>>"; } }

        IGpioAdapter _gpio;
        public OutputCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.SetDirection(number, PinDirection.Output);
            Console.WriteLine(string.Format("Pin {0} set to output", number));
        }
    }
}