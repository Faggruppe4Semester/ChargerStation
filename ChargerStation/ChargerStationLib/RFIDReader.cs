using System;
using ChargerStation.Interfaces;

namespace ChargerStation
{
    public class RfIdEventArgs : EventArgs
    {
        public int Id { get; set; }
    }

    public class RfIdReader : IRfIdReader
    {
        public event EventHandler<RfIdEventArgs> RfIdDetectedEvent;
        protected virtual void OnRfIdDetected(RfIdEventArgs e)
        {
            RfIdDetectedEvent?.Invoke(this, e);
        }

        public void Detect(int id)
        {
            OnRfIdDetected(new RfIdEventArgs{Id = id});
        }
    }
}