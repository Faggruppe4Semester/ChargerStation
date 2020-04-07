using System;
using ChargerStation.Interfaces;

namespace ChargerStation
{
    public class Display : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

}