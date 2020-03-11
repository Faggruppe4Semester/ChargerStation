using System;
using System.Diagnostics;
using System.IO;

namespace ChargerStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeskabState _state;
        private IUsbCharger _charger;
        private IDoor _door;
        private IDisplay _display;
        private IRfIdReader _rfIdReader;
        private Ilog _log;
        private int _oldId;
        
        public StationControl(IDoor door, IUsbCharger usbCharger, IRfIdReader rfIdReader)
        {
            _door = door;
            _charger = usbCharger;
            _rfIdReader = rfIdReader;
            _display = new Display();
            _log = new LogFile();
            _door.DoorStateChangedEvent += HandleDoorStateChangedEvent;
            _rfIdReader.RfIdDetectedEvent += HandleRfidDetectedEvent;

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _oldId = id;
                        _state = LadeskabState.Locked;
                        _charger.StartCharge();
                        _log.LogDoorLocked(id);
                        _display.DisplayMessage("Ladeskab optaget");
                    }
                    else
                    {
                      _display.DisplayMessage("Tilslutningsfejl");  
                    }
                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id != _oldId)
                    {
                        _display.DisplayMessage("Forkert RFID");
                    }
                    else
                    {
                        _charger.StopCharge();
                        _state = LadeskabState.Available;
                        _log.LogDoorUnlocked(id);
                        _display.DisplayMessage("Fjern telefon");
                    }

                    break;
            }
        }

        void HandleRfidDetectedEvent(object source, EventArgs e)
        {
            RfIdEventArgs args = (RfIdEventArgs) e;
            RfidDetected(args.Id);
        }
        
        // Her mangler de andre trigger handlere
        void HandleDoorStateChangedEvent(object source, EventArgs e)
        {
            DoorStateChangedEventArgs args = (DoorStateChangedEventArgs)e;
            if (args.State == DoorState.Open)
            {
                _display.DisplayMessage("Tilslut telefon");
                _state = LadeskabState.DoorOpen;
            }

            if (args.State == DoorState.Closed)
            {
                _display.DisplayMessage("Indlæs RFID");
                _state = LadeskabState.Available;
            }
        }
    }
}