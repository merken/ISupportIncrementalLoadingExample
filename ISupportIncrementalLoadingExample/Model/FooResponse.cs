using System.Collections.Generic;

namespace ISupportIncrementalLoadingExample.Model
{
    public class FooResponse
    {
        public bool HasMoreItems { get; set; }
        public IEnumerable<Foo> Foos { get; set; }
    }
}
