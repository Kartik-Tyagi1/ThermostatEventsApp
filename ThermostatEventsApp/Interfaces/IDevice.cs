using System;
using System.Collections.Generic;
using System.Text;

namespace ThermostatEventsApp.Interfaces
{
    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }
        void RunDevice();
        void HandleEmergency();
    }
}
