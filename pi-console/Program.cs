using System;
using System.Linq;
using System.Threading.Tasks;
using pi_dotnetcore.Gpio;

namespace pi_console
{
    class Program
    {
        static IGpio _gpio;

        static void Main(string[] args)
        {
            Console.WriteLine("Raspberry Pi Console Application\n");
            
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("missing required parameter.\n");
                Console.WriteLine("usage: pi-console list|init|deinit|status|on|off <pin>");
                Console.WriteLine("\n  ex: list all initialized pins:");
                Console.WriteLine("  pi-console list");
                Console.WriteLine("\n  ex: initialize pin 12:");
                Console.WriteLine("  pi-console init 12");
                Console.WriteLine("\n  ex: de-initialize pin 12:");
                Console.WriteLine("  pi-console deinit 12");
                Console.WriteLine("\n  ex: get status of pin 12:");
                Console.WriteLine("  pi-console status 12");
                Console.WriteLine("\n  ex: turn pin 16 on");
                Console.WriteLine("  pi-console on 16");
                Console.WriteLine("\n  ex: turn pin 18 off");
                Console.WriteLine("  pi-console off 18");
                return;
            }

            try
            {
                _gpio = new RaspberryPiGpio();

                switch (args[0].ToLower())
                {
                    case "list":
                        List();
                        return;

                    case "status":
                        Status(args);
                        return;

                    case "init":
                        Init(args);
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

                    default:
                        throw new ArgumentOutOfRangeException("command", args[0], "unknown command.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Done.");
        }
        static void List()
        {
            var pins = _gpio.GetInitializedPins().ToList();

            Console.WriteLine("No pins are initialized.");

            pins.ForEach(pin =>
                Console.WriteLine("Pin {0} (output: {1}, on: {2})", pin.number, pin.output, pin.on)
            );
        }
        static void Init(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.InitPin(number, true);
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
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            var pin = _gpio.GetPin(number);
            Console.WriteLine("Pin {0} (output: {1}, on: {2})", pin.number, pin.output, pin.on);
        }

        static void On(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetPin(number, true);
            Console.WriteLine(string.Format("Pin {0} turned on", number));
        }

        static void Off(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            _gpio.SetPin(number, false);
            Console.WriteLine(string.Format("Pin {0} turned off", number));
        }
    }
}
