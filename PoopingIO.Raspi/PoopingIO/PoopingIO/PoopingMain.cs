using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.Swan;

namespace PoopingIO
{
    class PoopingMain
    {
        public static void PMain()
        {
            var pin = Pi.Gpio[BcmPin.Gpio12];
            pin.PinMode = GpioPinDriveMode.Input;
            pin.RegisterInterruptCallback(EdgeDetection.FallingEdge, ISRCallback);
            Console.ReadKey();
        }

        static void ISRCallback()
        {
            "Pin Activated...".WriteLine();
        }

    }
}
