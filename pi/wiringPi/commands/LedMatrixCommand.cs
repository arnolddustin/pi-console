using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WiringPi;

namespace pi.wiringPi.commands
{
    /// <summary>
    /// Scrolls a message across an LED matrix display
    /// </summary>
    public class LedMatrixCommand : ICommand
    {
        #region ICommand Interface

        public string[] Name { get { return new string[] { "ledmatrix" }; } }
        public string Description { get { return "ledmatrix test command"; } }
        public string Usage { get { return "ledmatrix"; } }

        public void Run(params string[] args)
        {
            RunCommand();
        }

        #endregion

        double SCROLLDELAY = 0.05;
        const int LOOPS = 4;
        const int RCLK = 0;
        const int SRCLK = 1;
        const int SDI = 2;

        List<byte> data = new List<byte>();

        byte[] tab = new byte[] { 0xfe, 0xfd, 0xfb, 0xf7, 0xef, 0xdf, 0xbf, 0x7f };

        public LedMatrixCommand()
        {
            var p = new PixelArt();
            data.AddRange(p.Get('A'));
            data.AddRange(p.Get('B'));
            data.AddRange(p.Get("Jackson"));
            data.AddRange(p.Get(p.SupportedCharacters()));
        }

        public void RunCommand(params string[] args)
        {
            init();

            for (int l = 0; l < LOOPS; l++)
            {
                for (var i = 0; i < data.Count - 8; i++)
                {
                    var start = DateTime.Now;
                    while (true)
                    {
                        for (var j = 0; j < 8; j++)
                        {
                            hc595_in(data[i + j]);
                            hc595_in(tab[j]);
                            hc595_out();
                        }
                        if (DateTime.Now > start.AddSeconds(SCROLLDELAY))
                            break;
                    }
                }
            }
        }

        void init()
        {
            if (Init.WiringPiSetup() == -1)
                throw new ApplicationException("setup wiringPi failed!");

            GPIO.pinMode(SDI, 1);
            GPIO.pinMode(RCLK, 1);
            GPIO.pinMode(SRCLK, 1);

            GPIO.digitalWrite(SDI, 0);
            GPIO.digitalWrite(RCLK, 0);
            GPIO.digitalWrite(SRCLK, 0);
        }

        void hc595_in(byte dat)
        {
            for (var i = 0; i < 8; i++)
            {
                GPIO.digitalWrite(SDI, 0x80 & (dat << i));
                GPIO.digitalWrite(SRCLK, 1);
                GPIO.digitalWrite(SRCLK, 0);
            }
        }

        void hc595_out()
        {
            GPIO.digitalWrite(RCLK, 1);
            GPIO.digitalWrite(RCLK, 0);
        }
    }
}