using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace pi_console
{
    class GpioController
    {
        private IList<int> _validPins;

        public GpioController()
        {
            _validPins = new List<int>();
            _validPins.Add(12);
            _validPins.Add(16);
            _validPins.Add(18);
        }

        public void SetPin(int pin, bool on)
        {
            if (!_validPins.Any(x => x == pin))
            {
                throw new ArgumentOutOfRangeException("Invalid pin.");
            }

            var pinFolder = string.Format("gpio{0}", pin);

            // make sure pin is open
            if (!Directory.Exists("/sys/class/gpio/" + pinFolder))
            {
                File.WriteAllText("/sys/class/gpio/export", pin.ToString());
            }

            // configure pin for output
            File.WriteAllText("/sys/class/gpio/" + pinFolder + "/direction", "out");

            File.WriteAllText("/sys/class/gpio/" + pinFolder + "/value", (on ? 1 : 0).ToString());
        }
    }
}