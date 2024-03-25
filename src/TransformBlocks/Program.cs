using System.Threading.Tasks.Dataflow;

var transformBlock = new TransformBlock<int, string>(n =>
{
    Console.WriteLine($"Processing {n} ....");
    Task.Delay(10000).Wait();
    return n.ToString();
}, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism =10 });


for (int i = 0; i < 10; i++)
{
    transformBlock.Post(i);
    Console.WriteLine($"number of messages in input queue {transformBlock.InputCount}");
}

for (int i = 0; i < 10; i++)
{
    var result = transformBlock.Receive();

    Console.WriteLine($"number of messages in output queue {transformBlock.OutputCount}" +
        $" input queue {transformBlock.InputCount}");
}

Console.WriteLine("finished");
Console.ReadKey();