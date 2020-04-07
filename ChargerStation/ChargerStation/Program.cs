using System;
using ChargerStation.Interfaces;
using UsbSimulator;

namespace ChargerStation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var running = true;

            IDoor door = new Door();
            UsbChargerSimulator usbCharger = new UsbChargerSimulator();
            IChargerControl c = new ChargerControl(usbCharger, new Display());
            IRfIdReader rfid = new RfIdReader();
            Ilog log = new LogFile();
            IDisplay display = new Display();
            StationControl sc = new StationControl(door, c, rfid, log, display);
            
            Console.WriteLine("Ladeskab Tændt.");
            Console.WriteLine("Hvilken handling vil du foretage?");
            Console.WriteLine("Toggle (Åben/Lukket) Dør: {d}, Indlæs RFID: {r}, Toggle (Afbrudt/Forbundet) Tlf-forbindelse: {f}, Luk Programmet {q}");

            do
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case 'd':
                    case 'D':
                    {
                        Console.WriteLine("");
                        if (door.State == DoorState.Open) door.DoorClosed();
                        else door.DoorOpen();
                        break;
                    }

                    case 'r':
                    case 'R':
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Indlæs et RFID-Id");
                        rfid.Detect(Console.ReadKey().KeyChar);
                        Console.WriteLine("");
                        break;
                    }

                    case 'f':
                    case 'F':
                    {
                        Console.WriteLine("");
                        usbCharger.SimulateConnected(!usbCharger.Connected);
                        if(usbCharger.Connected) Console.WriteLine("Telefon-forbindelse Forbundet");
                        else Console.WriteLine("Telefon-forbindelse Afbrudt");
                        break;
                    }

                    case 'q':
                    case 'Q':
                    {
                        running = false;
                        break;
                    }

                    default:
                        break;
                }
            } while (running == true);
        }
    }
}