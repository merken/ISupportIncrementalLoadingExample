using ISupportIncrementalLoadingExample.Incremental;
using System;
using System.Collections.Generic;

namespace ISupportIncrementalLoadingExample.Model
{
    public class GroupedFooResponse
    {
        public bool HasMoreItems { get; set; }
        public IEnumerable<GroupModel<String, Foo>> GroupedFoos { get; set; }
    }
}
