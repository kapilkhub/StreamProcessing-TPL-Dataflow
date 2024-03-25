using System.Threading.Tasks.Dataflow;

var batchBlock = new BatchBlock<int>(3);

for (int i = 0; i < 10; i++)
{
    batchBlock.Post(i);
}
batchBlock.Complete();
batchBlock.Post(10);

for (int i = 0; i < 5; i++)
{
    int[] recieveValues;

    if (batchBlock.TryReceive(out recieveValues))
    {
        Console.WriteLine($"Recieved Block {i}");

        foreach (var item in recieveValues)
        {
            Console.Write($"{item} ");
        }
    }
    else 
    {
        Console.WriteLine("Batch Finished");
    }

    Console.WriteLine();    

}

