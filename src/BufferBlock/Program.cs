using System.Threading.Tasks.Dataflow;

var bufferBlock = new BufferBlock<int>( new ExecutionDataflowBlockOptions { BoundedCapacity =1 });

var actionBlock_1 = new ActionBlock<int>(n => { 

    Console.WriteLine($"Consumer 1 recieved message {n}");
    Task.Delay(300).Wait();
}, new ExecutionDataflowBlockOptions { BoundedCapacity = 1 });

var actionBlock_2 = new ActionBlock<int>(n => {

    Console.WriteLine($"Consumer 2 recieved message {n}");
    Task.Delay(300).Wait();
}, new ExecutionDataflowBlockOptions { BoundedCapacity =1 });


bufferBlock.LinkTo(actionBlock_1);
bufferBlock.LinkTo(actionBlock_2);

for (int i = 0; i < 10; i++)
{
    //if (bufferBlock.Post(i))
    //{
    //    Console.WriteLine("Message Pushed: " + i);
    //}
    //else
    //{
    //    Console.WriteLine("Message Lost: " + i);
    //}

    await bufferBlock.SendAsync(i);
    
}

Console.ReadKey();