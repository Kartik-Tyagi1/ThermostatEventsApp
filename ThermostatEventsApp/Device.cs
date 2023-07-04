using System;
using System.Collections.Generic;
using System.Text;
using ThermostatEventsApp.Interfaces;

namespace ThermostatEventsApp
{
    public class Device : IDevice
    {
        private const double WARNING_LEVEL = 27;
        private const double EMERGENCY_LEVEL = 75;

        public double WarningTemperatureLevel => WARNING_LEVEL;
        public double EmergencyTemperatureLevel => EMERGENCY_LEVEL;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Sending notifications to emergency servies personal...");
            ShutdownDevice();
            Console.WriteLine();
        }

        private void ShutdownDevice()
        {
            Console.WriteLine("Shutting down device...");
            Environment.Exit(0);
        }

        public void RunDevice()
        {
            Console.WriteLine("Device is Running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor(WARNING_LEVEL, EMERGENCY_LEVEL);
            IThermostat thermostat = new Thermostat(coolingMechanism, heatSensor, this);

            thermostat.RunThermostat();
        }
    }
}
