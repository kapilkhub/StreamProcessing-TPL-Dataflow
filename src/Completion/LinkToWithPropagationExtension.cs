using System.Threading.Tasks.Dataflow;

namespace Completion
{
    public static class LinkToWithPropagationExtension
    {
        public static IDisposable LinkToWithPropagation<T>(this ISourceBlock<T> sourceBlock, ITargetBlock<T> targetBlock)
        {
           return sourceBlock.LinkTo(targetBlock, new DataflowLinkOptions { PropagateCompletion = true });
        }
    }
}
