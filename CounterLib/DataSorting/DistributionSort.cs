using System;
using System.Collections.Generic;
using System.Linq;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataSorting
{
    public class DistributionSort<TSource> : ISorter<int, TSource>
    {
        public ICollection<TSource> Sort(ICollection<TSource> source, Func<TSource, int> keySelector, ISortSettings<int> settings)
        {
            var maxKey = settings?.MaxKey ?? source.Max(keySelector);
            var resultArray = new List<TSource>[maxKey + 1];

            foreach (var item in source)
            {
                var key = keySelector(item);
                var list = resultArray[key];
                if (list == null)
                {
                    list = new List<TSource>();
                    resultArray[key] = list;
                }
                list.Add(item);
            }

            var sortedList = new List<TSource>();
            for (var i = maxKey; i >= 0; i--)
            {
                var list = resultArray[i];
                if (list != null) sortedList.AddRange(list);
            }
            return sortedList;
        }
    }
}
