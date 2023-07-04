# ThermostatEventsApp
This application was created as part of the Advanced C# Tutorial Series created by Gavin Lon on youtube as means to learn about delegates and events in C#.

# About Events
Events in C# are used to respond to certain things. These things can just be calling code (like how it is in this project) or user actions like clicking on a button. In C# events are a type of delegate which lets you bind a function,
which is denoted as a "callback function" to something called an EventHandler (this is also known as subscribing to an event). When the calling code matches a certain condition (like how the temp reaches a certain range in this example) we can call a function which triggeres the function that was bound
to the eventhandler which in this project resembles "onTemp..."

This is a very intersting use of delegates and does make it much easier to call functions in respond to events as opposed to writing more logic to define when and how everything should work especially in larger scale projects.
