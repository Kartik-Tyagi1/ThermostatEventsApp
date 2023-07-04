using System;
using System.Collections.Generic;
using System.Text;
using ThermostatEventsApp.Interfaces;

namespace ThermostatEventsApp
{
    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSensor _heatSensor = null;
        private IDevice _device = null;


        public Thermostat(ICoolingMechanism coolingMechanism, IHeatSensor heatSensor, IDevice device)
        {
            _coolingMechanism = coolingMechanism;
            _heatSensor = heatSensor;
            _device = device;
        }

        private void WireUpEventsToEventHandlers()
        {
            // When the temperature is such that a event needs to be fired off, the onTemp... methods will see these are attached to the event handlers
            // and call the correponding methods

            _heatSensor.Temp_Falls_Below_Warning_Level_EventHandler += _heatSensor_Temp_Falls_Below_Warning_Level_EventHandler;
            _heatSensor.Temp_Reaches_Warning_Level_EventHandler += _heatSensor_Temp_Reaches_Warning_Level_EventHandler;
            _heatSensor.Temp_Reaches_Emergency_Level_EventHandler += _heatSensor_Temp_Reaches_Emergency_Level_EventHandler;
        }

        private void _heatSensor_Temp_Falls_Below_Warning_Level_EventHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information Alert!! Temperature falls below Warning Level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel}");
            _coolingMechanism.Off();
            Console.ResetColor();
        }

        private void _heatSensor_Temp_Reaches_Warning_Level_EventHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning Alert!! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel}");
            _coolingMechanism.On();
            Console.ResetColor();
        }

        private void _heatSensor_Temp_Reaches_Emergency_Level_EventHandler(object sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency Alert!! Temperature Above Emergency Level (Emergency level is {_device.EmergencyTemperatureLevel} and above");
            _device.HandleEmergency();
            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
            WireUpEventsToEventHandlers();
            _heatSensor.RunHeatSensor();

        }
    }
}
