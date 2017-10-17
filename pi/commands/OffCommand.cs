using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class OffCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "off" }; } }
        public string Description { get { return "Turns a pin off (sets value to LOW)"; } }
        public string Usage { get { return "off <<pin>>"; } }

        IGpioAdapter _gpio;
        public OffCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.SetValue(number, PinValue.Off);
            Console.WriteLine(string.Format("Pin {0} turned off", number));
        }
    }
}