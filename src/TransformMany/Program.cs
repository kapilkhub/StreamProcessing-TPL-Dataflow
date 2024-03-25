using System.Threading.Tasks.Dataflow;

var transformManyBlock = new TransformManyBlock<int, string>(n => FindEvenNumbers(n));
var printblock = new ActionBlock<string>(a => Console.WriteLine($"Recieved message: {a}"));

transformManyBlock.LinkTo(printblock);

for (int i = 0; i < 10; i++)
{
    transformManyBlock.Post(i);
}

Console.WriteLine("Finished");
Console.ReadKey();

IEnumerable<string> FindEvenNumbers(int number)
{
    for (int i = 0; i < number; i++)
    {
        if (i % 2 == 0) 
        {
            yield return $"{number}: {i}";
        }   
    }
}