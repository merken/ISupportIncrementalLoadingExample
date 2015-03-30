using System;

namespace ISupportIncrementalLoadingExample.Incremental
{
    public class HasMoreItemsEventArgs : EventArgs
    {
        public bool HasMoreItems { get; set; }
    }
}
