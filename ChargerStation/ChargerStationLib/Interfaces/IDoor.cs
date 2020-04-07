using System;

namespace ChargerStation.Interfaces
{
    public interface IDoor
    {
        DoorState State { get; set; }
        event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;
        void DoorClosed();
        void DoorOpen();
    }
}