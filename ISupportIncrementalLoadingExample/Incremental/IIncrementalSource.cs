using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISupportIncrementalLoadingExample.Incremental
{
    public interface IIncrementalSource<T>
    {
        bool HasMoreItemsFromServer { get; }

        Task<IEnumerable<T>> GetMoreItems(int pageIndex, int pageSize);

        void Reset();
    }
}
