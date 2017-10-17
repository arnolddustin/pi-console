using System;
using System.Linq;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class PhotoCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "photo" }; } }
        public string Description { get { return "Reads a photoresistor, returning the time (in ms) to charge/discharge the capacitor"; } }
        public string Usage { get { return "photo <<pin>>"; } }

        IGpioAdapter _gpio;
        public PhotoCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }
        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            _gpio.DeInitPin(number);
            _gpio.InitPin(number);
            _gpio.SetDirection(number, PinDirection.Output);
            _gpio.SetValue(number, PinValue.Off);
            _gpio.GetValue(number);

            _gpio.SetDirection(number, PinDirection.Input);
            var counter = 0;
            while (true)
            {
                if (_gpio.GetValue(number) == PinValue.On)
                    break;

                counter++;
            }

            Console.WriteLine(string.Format("Pin {0} photosensor reading is {1}", number, counter));
        }
    }
}