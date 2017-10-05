using System;
using System.Linq;
using System.Threading.Tasks;
using pi_dotnetcore.Gpio;

namespace pi_console
{
    class Program
    {
        static IGpioAdapter _gpio;

        static void Main(string[] args)
        {
            Console.WriteLine("Raspberry Pi Console Application\n");

            if (args == null || args.Length < 1)
            {
                Console.WriteLine("missing required parameter.\n");
                Console.WriteLine("usage: pi-console init|deinit|status|on|off|photo <pin>");
                Console.WriteLine("\n  ex: initialize pin 12:");
                Console.WriteLine("  pi-console init 12");
                Console.WriteLine("\n  ex: set pin 12 to output:");
                Console.WriteLine("  pi-console output 12");
                Console.WriteLine("\n  ex: set pin 12 to input:");
                Console.WriteLine("  pi-console input 12");
                Console.WriteLine("\n  ex: de-initialize pin 12:");
                Console.WriteLine("  pi-console deinit 12");
                Console.WriteLine("\n  ex: get status of pin 12:");
                Console.WriteLine("  pi-console status 12");
                Console.WriteLine("\n  ex: turn pin 16 on");
                Console.WriteLine("  pi-console on 16");
                Console.WriteLine("\n  ex: turn pin 18 off");
                Console.WriteLine("  pi-console off 18");
                Console.WriteLine("\n  ex: read the photo sensor on pin 18");
                Console.WriteLine("  pi-console photo 18");
                return;
            }

            try
            {
                _gpio = new FileSystemGpioAdapter();

                switch (args[0].ToLower())
                {
                    case "status":
                        Status(args);
                        return;

                    case "init":
                        Init(args);
                        return;

                    case "input":
                        Input(args);
                        return;

                    case "output":
                        Output(args);
                        return;

                    case "deinit":
                        DeInit(args);
                        return;

                    case "on":
                        On(args);
                        return;

                    case "off":
                        Off(args);
                        return;

                    case "photo":
                        Photo(args);
                        return;

                    case "photoloop":
                        Photoloop(args);
                        return;

                    default:
                        throw new ArgumentOutOfRangeException("command", args[0], "unknown command.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Done.");
        }

        static void Init(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");

            _gpio.InitPin(number);
            Console.WriteLine(string.Format("Pin {0} initialized", number));
        }

        static void DeInit(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.DeInitPin(number);
            Console.WriteLine(string.Format("Pin {0} deinitialized", number));
        }

        static void Status(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");

            var direction = _gpio.GetDirection(number);
            var value = _gpio.GetValue(number);

            Console.WriteLine("Pin {0} [direction: {1}, value: {2}]", number, direction, value);
        }

        static void Input(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetDirection(number, PinDirection.Input);
            Console.WriteLine(string.Format("Pin {0} set to input", number));
        }

        static void Output(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetDirection(number, PinDirection.Output);
            Console.WriteLine(string.Format("Pin {0} set to output", number));
        }

        static void On(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetValue(number, PinValue.On);
            Console.WriteLine(string.Format("Pin {0} turned on", number));
        }

        static void Off(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetValue(number, PinValue.Off);
            Console.WriteLine(string.Format("Pin {0} turned off", number));
        }

        static void Photo(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");

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

        static void Photoloop(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");

            while (true)
            {
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
                Task.Delay(100).Wait();
            }
        }
    }
}
