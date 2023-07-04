using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ThermostatEventsApp.Interfaces;

namespace ThermostatEventsApp
{
    public class HeatSensor : IHeatSensor
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;
        bool _hasReachedWarningTemp = false;

        // This is a specialized list that is optimized to store delegates
        protected EventHandlerList _listEventDelegates = new EventHandlerList();

        // When adding delegates to the EventHandlerList we must pass in an associated object which serves a key for the delegate 
        static readonly object _tempReachesWarningLevelKey = new object();
        static readonly object _tempFallsBelowWarningLevelKey = new object();
        static readonly object _tempReachesEmergencyLevelKey = new object();

        // Holds mock temperature data
        private double[] _temperatureData = null;

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;

            SeedData();
        }

        private void SeedData()
        {
            // Warning range is 27-75 Celsius
            _temperatureData = new double[] { 16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86.45 };
        }

        private void MonitorTemperature()
        {
            foreach (double temperature in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temperature} °C");

                if (temperature >= _emergencyLevel)
                {
                    TemperatureEventArgs args = new TemperatureEventArgs
                    {
                        CurrentDateTime = DateTime.Now,
                        Temperature = temperature
                    };

                    onTempReachesEmergencyLevel(args);
                }
                else if (temperature >= _warningLevel)
                {
                    _hasReachedWarningTemp = true;
                    TemperatureEventArgs args = new TemperatureEventArgs
                    {
                        CurrentDateTime = DateTime.Now,
                        Temperature = temperature
                    };

                    onTempReachesWarningLevel(args);
                }
                else if (temperature < _warningLevel && _hasReachedWarningTemp)
                {
                    _hasReachedWarningTemp = false;
                    TemperatureEventArgs args = new TemperatureEventArgs
                    {
                        CurrentDateTime = DateTime.Now,
                        Temperature = temperature
                    };

                    onTempFallsBelowWarningLevel(args);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }


        #region Functions That Get Triggered in Reponse to Events - onTemp... methods
        protected void onTempReachesWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> eventHandler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_tempReachesWarningLevelKey];
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        protected void onTempReachesEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> eventHandler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_tempReachesEmergencyLevelKey];
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        protected void onTempFallsBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> eventHandler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_tempFallsBelowWarningLevelKey];
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }
        #endregion

        #region Event Handlers
        /* 
         * For events the add and remove accessors are very similar to the get and set accessors that are implemented for c# read/write properties
         * 
         * add accessor - contains code that will fire when the client code subscribes to the relevant event
         * remove accessor - contains code that will fire when the client code unsubscribes from the relevant event
         * 
         * These are implemented when we implement an interface "explicitly"
         */

        event EventHandler<TemperatureEventArgs> IHeatSensor.Temp_Reaches_Emergency_Level_EventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_tempReachesEmergencyLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_tempReachesEmergencyLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.Temp_Reaches_Warning_Level_EventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_tempReachesWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_tempReachesWarningLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.Temp_Falls_Below_Warning_Level_EventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_tempFallsBelowWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_tempFallsBelowWarningLevelKey, value);
            }
        }

        #endregion

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat Sensor is running...");
            MonitorTemperature();
        }
    }

    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
