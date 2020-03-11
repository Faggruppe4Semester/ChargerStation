using System;
using System.IO;

namespace ChargerStation
{
    public class LogFile
    {
        public void LogDoorLocked(int LockID)
        {
            StreamWriter W  = File.AppendText("Log.txt");
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
        }

        public void LogDoorUnlocked(int LockID)
        {
            StreamWriter W = File.AppendText("Log.txt");
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
        }
    }
}