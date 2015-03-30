using ISupportIncrementalLoadingExample.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISupportIncrementalLoadingExample.Incremental.Sources
{
    public class GroupedFooSource : IIncrementalSource<GroupModel<String, Foo>>
    {
        private readonly Func<int, int,Task<GroupedFooResponse>> getGroupedFoos;

        private bool hasMoreItemsFromServer = true;
        public bool HasMoreItemsFromServer { get { return hasMoreItemsFromServer; } private set { hasMoreItemsFromServer = value; } }

        private DateTime now = DateTime.Now;

        public GroupedFooSource()
        {
        }

        public GroupedFooSource(Func<int, int,Task<GroupedFooResponse>> getGroupedFoos)
        {
            this.getGroupedFoos = getGroupedFoos;
        }

        public async Task<IEnumerable<GroupModel<string, Foo>>> GetMoreItems(int pageIndex, int pageSize)
        {
            var fooResponse = await getGroupedFoos(pageIndex, pageSize);
            if (fooResponse != null)
            {
                //If you want this call to be fired only ONCE, set this bool to FALSE
                //HasMoreItemsFromServer = false;
                //Other wise the server will provide us with a bool if more items are available
                HasMoreItemsFromServer = fooResponse.HasMoreItems;

                //The grouping is done server-side, this should be done client side to ensure that the items are grouped correctly
                //To do this, please implement the same call as FooSource to get items and do the grouping:
                //var grouped = from f in fooResponse.Foos
                //              group f by f.Category into g
                //              select new GroupModel<String, Foo> { Key = g.Key, Items = g.ToList() };
                //return grouped;

                return fooResponse.GroupedFoos;
            }
            return new List<GroupModel<string, Foo>>();//Return empty foos
        }


        public void Reset()
        {
            HasMoreItemsFromServer = true;
        }
    }
}
