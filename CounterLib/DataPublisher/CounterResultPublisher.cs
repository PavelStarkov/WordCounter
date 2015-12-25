using CounterLib.DataCounter.Base;
using CounterLib.DataPublisher.Base;

namespace CounterLib.DataPublisher
{
    public class CounterResultPublisher<TData> : IDataPublisher<ICounterResult<TData>>
    {
        private readonly IOutputDevice _outputDevice;

        public CounterResultPublisher(IOutputDevice outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public void Publish(ICounterResult<TData> data)
        {
            foreach (var ri in data.Items)
            {
               _outputDevice.Write($"{ri.Item} - {ri.TotalAmount}"); 
            }
        }
    }
}
