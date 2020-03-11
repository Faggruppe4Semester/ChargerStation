using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ChargerStation
{

    public interface Ilog
    {
        void LogDoorLocked(int LockID);
        void LogDoorUnlocked(int LockID);
    }
    public class LogFile : Ilog
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