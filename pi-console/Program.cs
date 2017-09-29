using System;
using System.Threading.Tasks;
using pi_dotnetcore;

namespace pi_console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("missing required parameter.\n");
                Console.WriteLine("usage: pi-console status|on|off <pin>");
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
                switch (args[0].ToLower())
                {
                    case "status":
                        Status(args);
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
            catch (ArgumentOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Done.");
        }

        static void Status(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            Console.WriteLine(new Gpio().Get(number).ToString());
        }

        static void On(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            new Gpio().Set(number, GpioDirection.Out, true);
            Console.WriteLine(string.Format("Pin {0} turned on", number));
        }

        static void Off(string[] args)
        {
            int number;
            if (!int.TryParse(args[1], out number))
            {
                throw new ArgumentOutOfRangeException("number", args[1], "argument 2 must be a number");
            }

            new Gpio().Set(number, GpioDirection.Out, false);
            Console.WriteLine(string.Format("Pin {0} turned off", number));
        }
    }
}
