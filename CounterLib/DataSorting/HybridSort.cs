using System;
using System.Collections.Generic;
using System.Linq;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataSorting
{
    public class HybridSort<TSource> : ISorter<int, TSource>
    {
        private readonly ISorter<int, TSource> _distributionSort;
        private readonly ISorter<int, TSource> _logarithmSort;

        public HybridSort(ISorter<int, TSource> distributionSort, ISorter<int, TSource> logarithmSort)
        {
            _distributionSort = distributionSort;
            _logarithmSort = logarithmSort;
        }

        public ICollection<TSource> Sort(ICollection<TSource> source, Func<TSource, int> keySelector, ISortSettings<int> settings)
        {
            var sourceCount = source.Count;
            var maxKey = settings?.MaxKey ?? source.Max(keySelector);
            var factor = 2 + maxKey/sourceCount;

            return (factor > Constants.MaxHybridSortFactor) || (sourceCount < 1 << factor)
                ? _logarithmSort.Sort(source, keySelector, settings)
                : _distributionSort.Sort(source, keySelector, settings);

        }
    }
}
