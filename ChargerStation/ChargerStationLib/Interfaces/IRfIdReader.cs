using System;

namespace ChargerStation.Interfaces
{
    public interface IRfIdReader
    {
        event EventHandler<RfIdEventArgs> RfIdDetectedEvent;
        void Detect(int id);
    }
}