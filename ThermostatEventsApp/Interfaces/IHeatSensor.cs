using System;
using System.Collections.Generic;
using System.Text;

namespace ThermostatEventsApp.Interfaces
{
    public interface IHeatSensor
    {
        // These events will hold references to functions and will be fired when the event is reached (aka temp reaches specific value)
        event EventHandler<TemperatureEventArgs> Temp_Reaches_Emergency_Level_EventHandler;
        event EventHandler<TemperatureEventArgs> Temp_Reaches_Warning_Level_EventHandler;
        event EventHandler<TemperatureEventArgs> Temp_Falls_Below_Warning_Level_EventHandler;

        void RunHeatSensor();
    }
}
