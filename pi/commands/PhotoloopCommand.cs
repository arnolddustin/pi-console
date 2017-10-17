using System;
using System.Linq;
using System.Threading.Tasks;
using pi_dotnetcore.Gpio;

namespace pi.commands
{
    public class PhotoloopCommand : PinCommand, ICommand
    {
        public string[] Name { get { return new string[] { "photoloop" }; } }
        public string Description { get { return "Experimental. Continuously reads a photoresistor."; } }
        public string Usage { get { return "photoloop <<pin>>"; } }

        IGpioAdapter _gpio;
        public PhotoloopCommand(IGpioAdapter gpio)
        {
            _gpio = gpio;
        }

        public void Run(params string[] args)
        {
            var number = GetPinNumber(args);

            while (true)
            {
                var counter = 0;

                // reset the pin
                _gpio.DeInitPin(number);
                _gpio.InitPin(number);

                // make output and turn off
                _gpio.SetDirection(number, PinDirection.Output);
                _gpio.SetValue(number, PinValue.Off);

                // change to input
                _gpio.SetDirection(number, PinDirection.Input);

                // count cycles until it turns on
                while (_gpio.GetValue(number) == PinValue.Off)
                    counter++;

                Console.WriteLine(string.Format("Pin {0} photosensor reading is {1}", number, counter));
                Task.Delay(10).Wait();
            }
        }
    }
}