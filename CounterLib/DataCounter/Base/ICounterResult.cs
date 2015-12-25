using System.Collections.Generic;

namespace CounterLib.DataCounter.Base
{
    public interface ICounterResult<out TData>
    {
        IEnumerable<ICounterResultItem<TData>> Items { get; }
    }
}
