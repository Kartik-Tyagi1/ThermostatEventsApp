using System;
using System.Collections.Generic;
using System.Text;
using ThermostatEventsApp.Interfaces;

namespace ThermostatEventsApp
{
    public class CoolingMechanism : ICoolingMechanism
    {
        public void Off()
        {
            Console.WriteLine();
            Console.WriteLine("Switching Cooler Off");
            Console.WriteLine();
        }

        public void On()
        {
            Console.WriteLine();
            Console.WriteLine("Switching Cooler On");
            Console.WriteLine();
        }
    }
}
