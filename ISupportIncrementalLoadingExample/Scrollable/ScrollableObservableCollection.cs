using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISupportIncrementalLoadingExample.Scrollable
{
    public class ScrollableObservableCollection<T> : ObservableCollection<T>, IScrollable
    {
        public ScrollableObservableCollection()
            : base() { }

        public ScrollableObservableCollection(IEnumerable<T> collection)
            : base(collection) { }

        private float verticalOffset = 1.0f;

        public float VerticalOffset { get { return verticalOffset; } set { verticalOffset = value; } }

        private float horizontalOffset = 0.0f;

        public float HorizontalOffset { get { return horizontalOffset; } set { horizontalOffset = value; } }

        private float zoomFactor = 1.0f;

        public float ZoomFactor { get { return zoomFactor; } set { zoomFactor = value; } }
    }
}
