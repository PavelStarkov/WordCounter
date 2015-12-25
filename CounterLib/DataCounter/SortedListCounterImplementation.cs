using System.Collections.Generic;
using CounterLib.DataCounter.Base;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataCounter
{
    public class SortedListCounterImplementation<TData> : DictionaryCounterImplementationBase<TData, SortedList<TData, int>>
    {
        public SortedListCounterImplementation(ISorter<int, ICounterResultItem<TData>> sorter) : base(sorter) { }
    }
}
