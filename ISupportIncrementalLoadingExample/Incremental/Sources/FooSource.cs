using ISupportIncrementalLoadingExample.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISupportIncrementalLoadingExample.Incremental.Sources
{
    public class FooSource : IIncrementalSource<Foo>
    {
        private readonly Func<int, int, Task<FooResponse>> getFoos;

        private bool hasMoreItemsFromServer = true;
        public bool HasMoreItemsFromServer { get { return hasMoreItemsFromServer; } private set { hasMoreItemsFromServer = value; } }

        public FooSource() //Default ctor
        {
        }

        public FooSource(Func<int, int, Task<FooResponse>> getFoos)
        {
            this.getFoos = getFoos;
        }
        
        public async Task<IEnumerable<Foo>> GetMoreItems(int pageIndex, int pageSize)
        {
            var fooResponse = await getFoos(pageIndex, pageSize);
            if (fooResponse != null)
            {
                //If you want this call to be fired only ONCE, set this bool to FALSE
                //HasMoreItemsFromServer = false;
                //Other wise the server will provide us with a bool if more items are available
                HasMoreItemsFromServer = fooResponse.HasMoreItems;
                return fooResponse.Foos;
            }
            return new List<Foo>();//Return empty foos
        }

        public void Reset()
        {
            HasMoreItemsFromServer = true;
        }
    }
}
