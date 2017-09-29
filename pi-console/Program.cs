using System;
using System.Threading.Tasks;

namespace pi_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to GPIO...");

            var ctl = new GpioController();
            var delay = 250;

            for (int i = 0; i < 10; i++)
            {
                ctl.SetPin(12, true);
                Task.Delay(delay).Wait();
                ctl.SetPin(16, true);
                Task.Delay(delay).Wait();
                ctl.SetPin(18, true);
                Task.Delay(delay).Wait();
                ctl.SetPin(12, false);
                Task.Delay(delay).Wait();
                ctl.SetPin(16, false);
                Task.Delay(delay).Wait();
                ctl.SetPin(18, false);
                Task.Delay(delay).Wait();
            }

            Console.WriteLine("Done.");
        }
    }
}
