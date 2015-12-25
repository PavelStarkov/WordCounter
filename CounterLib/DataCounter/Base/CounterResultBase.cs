using System.Collections.Generic;

namespace CounterLib.DataCounter.Base
{
    public abstract class CounterResultBase<TData> : ICounterResult<TData>
    {
        public IEnumerable<ICounterResultItem<TData>> Items
        {
            get; protected set;
        }
    }
}
