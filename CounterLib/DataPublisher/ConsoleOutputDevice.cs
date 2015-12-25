using System;
using CounterLib.DataPublisher.Base;

namespace CounterLib.DataPublisher
{
    public class ConsoleOutputDevice : IOutputDevice
    {
        public void Write(string str)
        {
           Console.WriteLine(str);
        }

        public void Write(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
