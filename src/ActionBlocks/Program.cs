using System.Threading.Tasks.Dataflow;

var actionBlock = new ActionBlock<int>(a => { 
    Task.Delay(500).Wait();
    Console.WriteLine(a);
});
for (int i = 0; i < 10; i++)
{
    actionBlock.Post(i);
    Task.Delay(300).Wait();
    Console.WriteLine($" number of messages in queue {actionBlock.InputCount}");
}

Console.WriteLine("Finish");
Console.ReadKey();