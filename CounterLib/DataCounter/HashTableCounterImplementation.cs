using System.Collections.Generic;
using CounterLib.DataCounter.Base;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataCounter
{
    public class HashTableCounterDataStructureImplementation<TData> : DictionaryCounterImplementationBase<TData, Dictionary<TData, int>>
    {
        public HashTableCounterDataStructureImplementation(ISorter<int, ICounterResultItem<TData>> sorter) : base(sorter) { }
    }
}
