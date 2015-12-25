using System;
using System.Collections.Generic;

namespace CounterLib.DataSorting.Base
{
    public interface ISorter<in TKey, TSource>
    {
        ICollection<TSource> Sort(ICollection<TSource> source, Func<TSource, TKey> keySelector, ISortSettings<TKey> settings);
    }
}
