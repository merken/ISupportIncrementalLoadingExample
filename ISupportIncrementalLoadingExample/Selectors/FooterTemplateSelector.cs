using ISupportIncrementalLoadingExample.Incremental;
using ISupportIncrementalLoadingExample.Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ISupportIncrementalLoadingExample.Selectors
{
    public class FooterTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template { get; set; }

        public DataTemplate FooterTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            DataTemplate template = Template;

            var listViewItem = container as ListViewItem;
            if (listViewItem != null)
            {
                var listView = ControlHelper.FindParent<ListViewBase>(listViewItem);
                if (listView != null)
                {
                    bool hasMoreItems = false;
                    if (listView.ItemsSource is ISupportIncrementalLoading)
                        hasMoreItems = (listView.ItemsSource as ISupportIncrementalLoading).HasMoreItems;
                    if (!hasMoreItems)
                    {
                        if (listView.ItemsSource is IIncrementalCollection)
                        {
                            var incrementalCollection = listView.ItemsSource as IIncrementalCollection;
                            if (incrementalCollection.IndexOfItem(listViewItem.DataContext) == incrementalCollection.ItemCount - 1)
                                template = FooterTemplate;
                        }
                    }
                }
            }
            return template;
        }
    }
}
