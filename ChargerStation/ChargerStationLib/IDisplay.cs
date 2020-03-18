using System;

namespace ChargerStation
{
    public interface IDisplay
    {
        void DisplayMessage(string message);
    }

    public class Display : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FakeDisplay : IDisplay
    {
        string content;
        public void DisplayMessage(string message)
        {
            content = message;
        }
    }
}