using System.Collections.Generic;
using System.Linq;
using CounterLib.DataSorting.Base;

namespace CounterLib.DataCounter.Base
{
    public class DictionaryCounterImplementationBase<TData, TDataType>  : ICounterDataStructureImplementation<TData, TData> where TDataType : IDictionary<TData, int>, new()
    {
        protected readonly TDataType Dic = new TDataType();
        protected readonly ISorter<int, ICounterResultItem<TData>> Sorter;
        public DictionaryCounterImplementationBase(ISorter<int, ICounterResultItem<TData>> sorter)
        {
            Sorter = sorter;
        }

        public virtual void AddData(TData data, int amount)
        {
            if (data != null)
            {
                int oldAmount;
                if (Dic.TryGetValue(data, out oldAmount))
                {
                    Dic[data] = amount + oldAmount;
                }
                else
                {
                    Dic.Add(data, amount);
                }
            }
        }

        public virtual ICounterResult<TData> GetResult()
        {
            return new DictionaryCounterResult(this);
        }

        private class DictionaryCounterResult : CounterResultBase<TData>
        {
            public DictionaryCounterResult(DictionaryCounterImplementationBase<TData, TDataType> dictionary)
            {
                var resultItems = dictionary.Dic.Select(
                    kv => new CounterResultItem<TData>(kv.Key, kv.Value) as ICounterResultItem<TData>
                    ).ToList();
                Items = (dictionary.Sorter != null) ? dictionary.Sorter.Sort(resultItems, cri => cri.TotalAmount, null) : resultItems;
            }
        }
    }
}
