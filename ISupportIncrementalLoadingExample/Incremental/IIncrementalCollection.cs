
namespace ISupportIncrementalLoadingExample.Incremental
{
    public interface IIncrementalCollection
    {
        int ItemCount { get; }
        int IndexOfItem(object item);
    }
}
