using ISupportIncrementalLoadingExample.Scrollable;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ISupportIncrementalLoadingExample.Incremental
{
    public class IncrementalCollection<T, I> : ObservableCollection<I>, IIncrementalCollection, IScrollable, ISupportIncrementalLoading, INotifyPropertyChanged
        where T : IIncrementalSource<I>
        where I : class
    {
        private T source;
        private int itemsPerPage;
        private int currentPage;
        private int itemCount = 0;

        public bool HasMoreItems
        {
            get;
            private set;
        }

        public int ItemCount
        {
            get { return itemCount; }
            private set { itemCount = value; RaisePropertyChanged(); }
        }

        public int IndexOfItem(object item)
        {
            var itemOfI = item as I;
            return this.IndexOf(itemOfI);
        }

        public event EventHandler<HasMoreItemsEventArgs> HasMoreItemsChanged;

        public IncrementalCollection()
        {
        }

        public IncrementalCollection(T source, int itemsPerPage = 20)
        {
            this.source = source;
            this.itemsPerPage = itemsPerPage;
            this.HasMoreItems = true;
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher dispatcher = Window.Current.CoreWindow.Dispatcher;
            return AsyncInfo.Run(async code =>
            {
                uint resultCount = 0;
                bool checkMoreItems = source.HasMoreItemsFromServer;
                if (checkMoreItems)
                {
                    var result = await source.GetMoreItems(currentPage, itemsPerPage);
                    currentPage++;
                    if (result != null && result.Count() == 0)
                    {
                        ItemCount = 0;
                        HasMoreItems = false;
                        if (HasMoreItemsChanged != null)
                            HasMoreItemsChanged(this, new HasMoreItemsEventArgs { HasMoreItems = HasMoreItems });
                    }
                    else
                    {
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            HasMoreItems = source.HasMoreItemsFromServer;
                            resultCount = (uint)result.Count();
                            ItemCount += (int)resultCount;

                            foreach (I item in result)
                                this.Add(item);
                        });
                    }
                    return new LoadMoreItemsResult()
                    {
                        Count = resultCount
                    };
                }
                else
                {
                    HasMoreItems = false;
                    if (HasMoreItemsChanged != null)
                        HasMoreItemsChanged(this, new HasMoreItemsEventArgs { HasMoreItems = HasMoreItems });
                    return new LoadMoreItemsResult()
                    {
                        Count = 0
                    };
                }
            });
        }

        protected override void ClearItems()
        {
            this.currentPage = 0;
            this.HasMoreItems = true;
            this.ItemCount = 0;
            this.source.Reset();
            if (HasMoreItemsChanged != null)
                HasMoreItemsChanged(this, new HasMoreItemsEventArgs { HasMoreItems = this.HasMoreItems });
            base.ClearItems();
        }

        private float verticalOffset = 1.0f;

        public float VerticalOffset { get { return verticalOffset; } set { verticalOffset = value; } }

        private float horizontalOffset = 0.0f;

        public float HorizontalOffset { get { return horizontalOffset; } set { horizontalOffset = value; } }

        private float zoomFactor = 1.0f;

        public float ZoomFactor { get { return zoomFactor; } set { zoomFactor = value; } }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
