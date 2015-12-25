using System;
using System.Collections.Generic;
using System.Linq;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataSorting
{
    public class QuickSort<TKey, TSource> : ISorter<TKey, TSource>
    {
        public ICollection<TSource> Sort(ICollection<TSource> source, Func<TSource, TKey> keySelector, ISortSettings<TKey> settings)
        {
            return source.OrderByDescending(keySelector).ToList();
        }
    }
}
