using System;
using System.IO;
using System.Runtime.CompilerServices;
using ChargerStation.Interfaces;

namespace ChargerStation
{

    public class LogFile : Ilog
    {
        public void LogDoorLocked(int LockID)
        {
            StreamWriter W  = File.AppendText("Log.txt");
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
            W.Close();
        }

        public void LogDoorUnlocked(int LockID)
        {
            StreamWriter W = File.AppendText("Log.txt");
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
            W.Close();
        }
    }
}