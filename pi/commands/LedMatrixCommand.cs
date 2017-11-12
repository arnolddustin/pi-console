using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WiringPi;

namespace pi.commands
{
    /// <summary>
    /// Scrolls a message across an LED matrix display
    /// </summary>
    /// <remarks>
    /// Adapted from Adeept LED Matrix C sample
    /// https://github.com/adeept/Adeept_Ultimate_Starter_Kit_C_Code_for_RPi/blob/master/16_ledMatrix/ledMatrix.c
    /// </remarks>
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

        /// <summary>
        /// memory clock input (STCP)
        /// </summary>
        const int RCLK = 0;

        /// <summary>
        /// shift register clock input (SHCP)
        /// </summary>
        const int SRCLK = 1;

        /// <summary>
        /// serial data input
        /// </summary>
        const int SDI = 2;

        byte[] data = new byte[] {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00, //NULL
        		0x00,0x00,0x3C,0x42,0x42,0x3C,0x00,0x00, //0
        		0x00,0x00,0x00,0x44,0x7E,0x40,0x00,0x00, //1
        		0x00,0x00,0x44,0x62,0x52,0x4C,0x00,0x00, //2
        		0x00,0x00,0x78,0x14,0x12,0x14,0x78,0x00, //A
        		0x00,0x00,0x60,0x90,0x90,0xFE,0x00,0x00, //d
        		0x00,0x00,0x1C,0x2A,0x2A,0x2A,0x24,0x00, //e
        		0x00,0x00,0x1C,0x2A,0x2A,0x2A,0x24,0x00, //e
        		0x00,0x00,0x7E,0x12,0x12,0x0C,0x00,0x00, //p
        		0x00,0x00,0x08,0x7E,0x88,0x40,0x00,0x00, //t
        		0x3C,0x42,0x95,0xB1,0xB1,0x95,0x42,0x3C, //:)
        		0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00  //NULL
            };

        byte[] tab = new byte[] { 0xfe, 0xfd, 0xfb, 0xf7, 0xef, 0xdf, 0xbf, 0x7f };

        public void RunCommand(params string[] args)
        {
            init();

            while (true)
            {
                for (var i = 0; i < data.Length - 8; i++)
                {
                    for (var k = 0; k < 3; k++)
                    {
                        for (var j = 0; j < 8; j++)
                        {
                            hc595_in(data[i + j]);
                            hc595_in(tab[j]);
                            hc595_out();
                            Task.Delay(1).Wait();
                        }
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