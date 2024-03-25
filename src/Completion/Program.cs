using Completion;
using System.Threading.Tasks.Dataflow;

var broadCastBlock = new BroadcastBlock<int>(a => a);


var transformBlock_1 = new TransformBlock<int, int>(a =>
{
    Console.WriteLine($"Message was produced by Consumer 1 message number : {a}");
    Task.Delay(1000).Wait();
    return a;
});

var transformBlock_2 = new TransformBlock<int, int>(a =>
{
    Console.WriteLine($"Message was produced by Consumer 2 message number : {a}");
    return a;
});

broadCastBlock.LinkToWithPropagation(transformBlock_1);
broadCastBlock.LinkToWithPropagation(transformBlock_2);
var joinBlock = new JoinBlock<int, int>();

transformBlock_1.LinkToWithPropagation(joinBlock.Target1);
transformBlock_2.LinkToWithPropagation(joinBlock.Target2);



var printblock = new ActionBlock<Tuple<int, int>>(a => Console.WriteLine($"message {a} was recieved"));


joinBlock.LinkToWithPropagation(printblock);

joinBlock.Completion.ContinueWith(j => Console.WriteLine("join block completed"));
printblock.Completion.ContinueWith(j => Console.WriteLine("print block completed"));
broadCastBlock.Completion.ContinueWith(j => Console.WriteLine("broadcast block completed"));
transformBlock_1.Completion.ContinueWith(t => Console.WriteLine("tb1 completed"));
transformBlock_2.Completion.ContinueWith(t => Console.WriteLine("tb2 completed"));

for (int i = 0; i < 10; i++)
{
    await broadCastBlock.SendAsync(i);
}

broadCastBlock.Complete();
await printblock.Completion;

