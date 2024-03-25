using System.Threading.Tasks.Dataflow;

var broadCastBlock = new BroadcastBlock<int>(a => a);


var transformBlock_1 = new TransformBlock<int, int>(a => {
    Console.WriteLine($"Message was produced by Consumer 1 message number : {a}");
    Task.Delay(1000).Wait();
    return a;
});

var transformBlock_2 = new TransformBlock<int, int>(a => {
    Console.WriteLine($"Message was produced by Consumer 2 message number : {a}");
    return a;
});

broadCastBlock.LinkTo(transformBlock_1);
broadCastBlock.LinkTo(transformBlock_2);
var batchJoinedBlock = new BatchedJoinBlock<int, int>(3);

transformBlock_1.LinkTo(batchJoinedBlock.Target1);
transformBlock_2.LinkTo(batchJoinedBlock.Target2);

var printblock = new ActionBlock<Tuple<IList<int>, IList<int>>>(a =>
{
    Console.WriteLine($"Message processed by block 1 [{string.Join(",", a.Item1)}], Message processed by block 2 [{string.Join(",", a.Item2)}]");


 });

batchJoinedBlock.LinkTo(printblock);

for (int i = 0; i < 10; i++)
{
    await broadCastBlock.SendAsync(i);
}
Console.WriteLine("Finished!");
Console.ReadKey();