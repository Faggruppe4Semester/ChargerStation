using System;

namespace ChargerStation
{
    public enum DoorState
    {
        Closed,Open
    }
    public interface IDoor
    {
        DoorState State { get; set; }
        event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent; 
        void DoorClosed();
        void DoorOpen();
    }
    
    public class Door : IDoor
    {
        public DoorState State { get; set; }
        public event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;
        protected virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }
        public void DoorClosed()
        {
            State = DoorState.Closed;
            OnDoorStateChanged(new DoorStateChangedEventArgs{ State = DoorState.Closed});
        }
        public void DoorOpen()
        {
            State = DoorState.Open;
            OnDoorStateChanged(new DoorStateChangedEventArgs{ State = DoorState.Open});
        }
    }
    
    public class DoorStateChangedEventArgs : EventArgs
    {
        public DoorState State { get; set; }
    }
}