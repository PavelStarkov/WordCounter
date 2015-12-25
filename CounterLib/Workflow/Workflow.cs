using CounterLib.DataCounter.Base;
using CounterLib.DataPublisher.Base;
using CounterLib.Workflow.Base;

namespace CounterLib.Workflow
{
    public class Workflow<TData> : IWorkflow
    {
       private readonly IGetCounterResult<TData> _counter;
       private readonly IDataPublisher<ICounterResult<TData>> _counterDataPublisher;

        public Workflow(
            IDataPublisher<ICounterResult<TData>> counterDataPublisher, IGetCounterResult<TData> counter)
        {
            _counterDataPublisher = counterDataPublisher;
            _counter = counter;
        }

        public void Proceed()
        {
            var result = _counter.GetResult();

            _counterDataPublisher.Publish(result);
        }
    }
}
