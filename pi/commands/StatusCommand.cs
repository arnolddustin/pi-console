using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class StatusCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "status" }; } }
        public string Description { get { return "Reads the status of a pin"; } }
        public string Usage { get { return "status <<pin>>"; } }

        IGpioAdapter _gpio;
        public StatusCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }
        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            var direction = _gpio.GetDirection(number);
            var value = _gpio.GetValue(number);

            Console.WriteLine("Pin {0} [direction: {1}, value: {2}]", number, direction, value);
        }
    }
}