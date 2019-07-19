using System;
using System.Collections.Generic;
using System.Linq;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;
using Unosquare.Swan;
using Unosquare.Swan.Networking;


namespace PoopingIO
{
    class Program
    {
        private const string postUrl = "http://21b00c23.ngrok.io/api/bathroom/update";
        private const int mBathId = 1;
        private const int wBathId = 2;

        //Pins used to create the doors buttons
        private static IGpioPin door0Pin = Pi.Gpio[BcmPin.Gpio12];
        private static IGpioPin door1Pin = Pi.Gpio[BcmPin.Gpio05];
        private static IGpioPin door2Pin = Pi.Gpio[BcmPin.Gpio06];
        private static IGpioPin door3Pin = Pi.Gpio[BcmPin.Gpio13];

        private static Dictionary<string, bool> mDoorsState = new Dictionary<string, bool>()
        {
            { "door 0", door0Pin.Read() },
            { "door 1", door1Pin.Read() },
        };

        private static Dictionary<string, bool> wDoorsState = new Dictionary<string, bool>()
        {
            { "door 2", door2Pin.Read() },
            { "door 3", door3Pin.Read() },
        };

        static void Main(string[] args)
        {
           // Pi.Init<BootstrapWiringPi>();

            InitialSync();

            var mbDoorsButtons = new Dictionary<string, Button>()
              {
                  { "door 0", new Button(door0Pin) },
                  { "door 1", new Button(door1Pin) },
              };

            var wbDoorsButtons = new Dictionary<string, Button>()
            {
                { "door 2", new Button(door2Pin) },
                { "door 3", new Button(door3Pin) },
            };

            mbDoorsButtons.ForEach((key, value) =>
            {
                value.Pressed += (s, e) =>
                {
                    mDoorsState[key] = true;
                    var availableDoors = mDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
                value.Released += (s, e) =>
                {
                    mDoorsState[key] = false;
                    var availableDoors = mDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
            });

            wbDoorsButtons.ForEach((key, value) =>
            {
                value.Pressed += (s, e) =>
                {
                    wDoorsState[key] = true;
                    var availableDoors = wDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
                value.Released += (s, e) =>
                {
                    wDoorsState[key] = false;
                    var availableDoors = wDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
            });

            Console.ReadKey();
        }
    
        static void InitialSync()
        {
            var mbavailable = mDoorsState.Where(x => x.Value == true).ToArray().Length;
            var wbavailable = wDoorsState.Where(x => x.Value == true).ToArray().Length;

            JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{mbavailable}", new { }).GetAwaiter().GetResult();
            JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{wbavailable}", new { }).GetAwaiter().GetResult();
        }
    }
}
