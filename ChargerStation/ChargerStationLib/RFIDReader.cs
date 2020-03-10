using System;

namespace ChargerStation
{
    public class RfIdEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    
    public interface IRfIdReader
    {
        event EventHandler<RfIdEventArgs> RfIdDetectedEvent;
        void Detect(int id);
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