using ISupportIncrementalLoadingExample.Scrollable;
using System;
using System.Collections.Generic;
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
    public class IncrementalGroupedCollection<T, TKey, TItem> : ObservableCollection<GroupModel<TKey, TItem>>, ISupportIncrementalLoading, IScrollable, IIncrementalCollection, INotifyPropertyChanged
        where T : IIncrementalSource<GroupModel<TKey, TItem>>
        where TKey : class, IComparable
        where TItem : class
    {
        private T source;
        private int itemsPerPage;
        private int currentPage;
        private List<TItem> flattenedList;
        private int itemCount = 0;

        public bool HasMoreItems
        {
            get;
            private set;
        }

        public event EventHandler<HasMoreItemsEventArgs> HasMoreItemsChanged;

        public IncrementalGroupedCollection(T source, int itemsPerPage = 20)
        {
            this.source = source;
            this.itemsPerPage = itemsPerPage;
            this.HasMoreItems = true;
            this.flattenedList = new List<TItem>();
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
                    HasMoreItems = source.HasMoreItemsFromServer;
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
                            resultCount = (uint)result.Count();
                            ItemCount += (int)resultCount;
                            foreach (GroupModel<TKey, TItem> group in result)
                            {
                                //Add to flattenedList
                                flattenedList.AddRange(group.Items);

                                //Check group
                                var groupInCollection = this.FirstOrDefault(i => i.Key == group.Key);
                                if (groupInCollection != null)
                                    foreach (TItem item in group.Items)
                                        groupInCollection.Items.Add(item);
                                else
                                    this.Add(group);
                            }
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

        public int FlattenedItemCount
        {
            get { return flattenedList.Count(); }
        }

        public int IndexOfItemInFlattenedList(object item)
        {
            var groupModel = item as GroupModel<TKey, TItem>;
            if (groupModel != null)
                return flattenedList.IndexOf(groupModel.Items.LastOrDefault());

            var tItem = item as TItem;
            return flattenedList.IndexOf(tItem);
        }

        public int ItemCount
        {
            get { return itemCount; }
            private set { itemCount = value; RaisePropertyChanged(); }
        }

        public int IndexOfItem(object item)
        {
            var itemOfI = item as GroupModel<TKey, TItem>;
            return this.IndexOf(itemOfI);
        }

        public TItem GetItem(int i)
        {
            return flattenedList[i];
        }

        public GroupModel<TKey, TItem> GetGroupFromItem(TItem item)
        {
            var group = this.FirstOrDefault(t => t.Items.Contains(item));
            return group;
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
