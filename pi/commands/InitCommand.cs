using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class InitCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "init" }; } }
        public string Description { get { return "Initializes a pin"; } }
        public string Usage { get { return "init <<pin>>"; } }

        IGpioAdapter _gpio;
        public InitCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.InitPin(number);
            Console.WriteLine(string.Format("Pin {0} initialized", number));
        }
    }
}