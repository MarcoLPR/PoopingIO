using System;
using System.Collections.Generic;
using System.Net.Http;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;
using Unosquare.Swan;
using Unosquare.Swan.Networking;

namespace PoopingIO
{
    class Program
    {

        static void Main(string[] args)
        {
            var postUrl = "";

            //Pins used to read the doors state
            var door0Pin = Pi.Gpio[BcmPin.Gpio12];
            var door1Pin = Pi.Gpio[BcmPin.Gpio05];
            var door2Pin = Pi.Gpio[BcmPin.Gpio06];
            var door3Pin = Pi.Gpio[BcmPin.Gpio13];

            var doorsState = new Dictionary<string, bool>()
            {
                    { "door 0", door0Pin.Read() },
                    { "door 1", door1Pin.Read() },
                    { "door 2", door2Pin.Read() },
                    { "door 3", door3Pin.Read() },
            };

            var doorsButtons = new Dictionary<string, Button>()
            {
                { "door 0", new Button(door0Pin) },
                { "door 1", new Button(door1Pin) },
                { "door 2", new Button(door2Pin) },
                { "door 3", new Button(door3Pin) },
            };
            var response = "";
            doorsButtons.ForEach((key, value) =>
            {
                value.Pressed += (s, e) =>
                {
                    doorsState[key] = true;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl, doorsState); //needs to be waited and validate if the server is working
                };
                value.Released += (s, e) =>
                {
                    doorsState[key] = false;
                    JsonClient.Post<Dictionary<string, bool>>(postUrl, doorsState);
                };

            });



        }
    }
}
