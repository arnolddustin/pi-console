using System;
using System.Linq;
using System.Threading.Tasks;
using WiringPi;

namespace pi.wiringPi.commands
{
    public class BlinkLedCommand : ICommand
    {
        #region ICommand Interface

        public string[] Name { get { return new string[] { "blink" }; } }
        public string Description { get { return "blink an LED on physical pin 22 (wiringPi 6)"; } }
        public string Usage { get { return "blink"; } }

        public void Run(params string[] args)
        {
            BlinkLED();
        }

        #endregion

        const int PIN = 6;

        public void BlinkLED()
        {
            if (Init.WiringPiSetup() == -1)
                throw new ApplicationException("Error initializing GPIO");

            GPIO.pinMode(PIN, 1);

            for (int i = 0; i < 25; i++)
            {
                GPIO.digitalWrite(PIN, 0);
                Task.Delay(200).Wait();
                GPIO.digitalWrite(PIN, 1);
                Task.Delay(100).Wait();
            }
        }
    }
}