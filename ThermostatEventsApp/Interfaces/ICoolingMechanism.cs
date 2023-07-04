using System;
using System.Collections.Generic;
using System.Text;

namespace ThermostatEventsApp.Interfaces
{
    public interface ICoolingMechanism
    {
        void On();
        void Off();
    }
}
