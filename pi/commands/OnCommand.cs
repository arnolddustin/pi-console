using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class OnCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "on" }; } }
        public string Description { get { return "Turns a pin on (sets value to HIGH)"; } }
        public string Usage { get { return "on <<pin>>"; } }

        IGpioAdapter _gpio;
        public OnCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.SetValue(number, PinValue.On);
            Console.WriteLine(string.Format("Pin {0} turned on", number));
        }
    }
}