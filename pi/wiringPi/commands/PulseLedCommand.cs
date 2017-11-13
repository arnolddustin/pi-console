using System;
using System.Linq;
using System.Threading.Tasks;
using WiringPi;

namespace pi.wiringPi.commands
{
    public class PulseLedCommand : ICommand
    {
        #region ICommand Interface

        public string[] Name { get { return new string[] { "pulse" }; } }
        public string Description { get { return "pulse an LED on physical pin 32 (wiringPi 26)"; } }
        public string Usage { get { return "pulse"; } }

        public void Run(params string[] args)
        {
            PulseLED();
        }

        #endregion

        const int PIN = 26;

        public void PulseLED()
        {
            if (Init.WiringPiSetup() == -1)
                throw new ApplicationException("Error initializing GPIO");

            GPIO.pinMode(PIN, 2); // pwm output

            for (var j = 0; j < 5; j++)
            {
                for (var i = 1023; i >= 0; i = i - 8)
                {
                    GPIO.pwmWrite(PIN, i);
                    Task.Delay(1).Wait();
                }
                for (var i = 0; i < 1024; i = i + 8)
                {
                    GPIO.pwmWrite(PIN, i);
                    Task.Delay(1).Wait();
                }
            }
        }
    }
}