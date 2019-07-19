using System;
using System.Collections.Generic;
using System.Linq;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.Swan;
using Unosquare.Swan.Networking;
using Unosquare.WiringPi;

namespace PoopingIO
{
    class Program
    {
        private const string postUrl = "http://21b00c23.ngrok.io/api/bathroom/update";
        private const int mBathId = 1;
        private const int wBathId = 2;




        static void Main(string[] args)
        {
            Pi.Init<BootstrapWiringPi>();
            
            //Pins used to create the doors buttons
        var door0Pin = Pi.Gpio[BcmPin.Gpio12];
        var door1Pin = Pi.Gpio[BcmPin.Gpio05];
        var door2Pin = Pi.Gpio[BcmPin.Gpio06];
        var door3Pin = Pi.Gpio[BcmPin.Gpio13];

            var mDoorsState = new Dictionary<string, bool>()
        {
            { "door 0", !door0Pin.Read() },
            { "door 1", !door1Pin.Read()},
        };

        var wDoorsState = new Dictionary<string, bool>()
        {
            { "door 2", !door2Pin.Read()},
            { "door 3", !door3Pin.Read()},
        };

            "antes del sync".Info();
            InitialSync(mDoorsState, wDoorsState);
            "despues del sync".Info();

            var mbDoorsButtons = new Dictionary<string, Button>()
              {
                  { "door 0", new Button(door0Pin, GpioPinResistorPullMode.PullDown) },
                  { "door 1", new Button(door1Pin, GpioPinResistorPullMode.PullDown) },
              };

            var wbDoorsButtons = new Dictionary<string, Button>()
            {
                { "door 2", new Button(door2Pin, GpioPinResistorPullMode.PullDown) },
                { "door 3", new Button(door3Pin, GpioPinResistorPullMode.PullDown) },
            };
            "buttons".Info();
            mbDoorsButtons.ForEach((key, value) =>
            {
                value.Pressed += (s, e) =>
                {
                    mDoorsState[key] = false;
                    var availableDoors = mDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
                value.Released += (s, e) =>
                {
                    mDoorsState[key] = true;
                    var availableDoors = mDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
            });

            wbDoorsButtons.ForEach((key, value) =>
            {
                value.Pressed += (s, e) =>
                {
                    wDoorsState[key] = false;
                    var availableDoors = wDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
                value.Released += (s, e) =>
                {
                    wDoorsState[key] = true;
                    var availableDoors = wDoorsState.Where(x => x.Value == true).ToArray().Length;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{availableDoors}", new { }).GetAwaiter().GetResult();
                };
            });

            Console.ReadKey();
        }
    
        static void InitialSync(Dictionary<string, bool> mDoorsState, Dictionary<string, bool>  wDoorsState )
        {
            var mbavailable = mDoorsState.Where(x => x.Value == true).ToArray().Length;
            var wbavailable = wDoorsState.Where(x => x.Value == true).ToArray().Length;

            
            JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{mBathId}/{mbavailable}", new { }).GetAwaiter().GetResult();
            JsonClient.Post<Dictionary<string, bool>>(postUrl + $"/{wBathId}/{wbavailable}", new { }).GetAwaiter().GetResult();
        }

            //PoopingMain.PMain();
            //Console.ReadKey();
        
    }
}
