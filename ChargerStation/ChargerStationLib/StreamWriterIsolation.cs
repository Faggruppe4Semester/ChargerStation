using System.IO;
using ChargerStation.Interfaces;

namespace ChargerStation
{
    public class StreamWriterIsolation : IStreamWriter
    {
        private StreamWriter writer = File.AppendText("Log.txt");

        public void Write(string text)
        {
            writer.Write(text);
        }

        public void WriteLine(string line)
        {
            writer.WriteLine(line);
        }

        public void Close()
        {
            writer.Close();
        }
    }
}