using System.Threading.Tasks.Dataflow;

var broadCastBlock = new BroadcastBlock<int>(a => a);


var transformBlock_1 = new TransformBlock<int, int>(a => {
    Console.WriteLine($"Message was produced by Consumer 1 message number : {a}");
    Task.Delay( 1000 ).Wait();
    return a;
    });

var transformBlock_2 = new TransformBlock<int, int>(a => {
    Console.WriteLine($"Message was produced by Consumer 2 message number : {a}");
    return a;
});

broadCastBlock.LinkTo(transformBlock_1);
broadCastBlock.LinkTo(transformBlock_2);
var joinBlock = new JoinBlock<int, int>();

transformBlock_1.LinkTo(joinBlock.Target1);
transformBlock_2.LinkTo(joinBlock.Target2);

var printblock = new ActionBlock<Tuple<int, int>>(a => Console.WriteLine($"message {a} was recieved"));

joinBlock.LinkTo(printblock);

for (int i = 0; i < 10; i++)
{
    await broadCastBlock.SendAsync(i);
}
Console.WriteLine("Finished!");
Console.ReadKey();