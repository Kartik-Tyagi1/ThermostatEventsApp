using System;
using System.ComponentModel;
using ThermostatEventsApp.Interfaces;

namespace ThermostatEventsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start device...");
            Console.ReadKey();

            IDevice device = new Device();
            device.RunDevice();

            Console.ReadKey();
        }
    }
}
