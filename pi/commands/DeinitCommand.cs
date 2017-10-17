using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class DeinitCommand : PinCommand, ICommand
    {
        IGpioAdapter _gpio;

        public DeinitCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public string[] Name { get { return new string[] { "deinit" }; } }
        public string Description { get { return "Deinitializes a pin"; } }
        public string Usage { get { return "deinit <<pin>>"; } }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.DeInitPin(number);
            Console.WriteLine(string.Format("Pin {0} deinitialized", number));
        }
    }
}