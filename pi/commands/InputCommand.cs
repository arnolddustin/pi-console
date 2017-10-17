using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class InputCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "input" }; } }
        public string Description { get { return "Sets a pin to input"; } }
        public string Usage { get { return "input <<pin>>"; } }

        IGpioAdapter _gpio;
        public InputCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.SetDirection(number, PinDirection.Input);
            Console.WriteLine(string.Format("Pin {0} set to input", number));
        }
    }
}