using System;
using System.Diagnostics;
using CounterLib.Workflow;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            var workflowSettings = new Arguments(args);
            if (workflowSettings.IsValid)
            {
                var workflowFactory = new WorkflowFactory();
                var counterWorkflow = workflowFactory.GetWorkflow(workflowSettings);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                counterWorkflow.Proceed();

                stopwatch.Stop();
                Console.WriteLine(@"RunTime: {0}", stopwatch.Elapsed);
            }
        }
    }
}
