using System;
using System.IO;
using System.Runtime.CompilerServices;
using ChargerStation.Interfaces;

namespace ChargerStation
{

    public class LogFile : Ilog
    {
        public LogFile(IStreamWriter streamWriter)
        {
            W = streamWriter;
        }

        private IStreamWriter W;
        public void LogDoorLocked(int LockID)
        {
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToShortTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
            W.Close();
        }

        public void LogDoorUnlocked(int LockID)
        {
            W.Write("Log entry: ");
            W.WriteLine($"{DateTime.Now.ToShortTimeString()} {DateTime.Now.ToLongDateString()}");
            W.WriteLine($"Charging station locked using RFID: {LockID}");
            W.WriteLine("-------------------------------------------------------");
            W.Close();
        }
    }
}