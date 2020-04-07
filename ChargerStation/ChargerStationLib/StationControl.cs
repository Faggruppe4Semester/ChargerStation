using System;
using System.Diagnostics;
using System.IO;

namespace ChargerStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeskabState _state;
        private IChargerControl _chargeControl;
        private IDoor _door;
        private IDisplay _display;
        private IRfIdReader _rfIdReader;
        private Ilog _log;
        private int _oldId;
        
        public StationControl(IDoor door, IChargerControl chargeControl, IRfIdReader rfIdReader, Ilog log, IDisplay display)
        {
            _door = door;
            _chargeControl = chargeControl;
            _rfIdReader = rfIdReader;
            _display = display;
            _log = log;
            _state = LadeskabState.Available;
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
                    if (_chargeControl.IsConnected())
                    {
                        _oldId = id;
                        _state = LadeskabState.Locked;
                        _chargeControl.StartCharge();
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
                        _chargeControl.StopCharge();
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