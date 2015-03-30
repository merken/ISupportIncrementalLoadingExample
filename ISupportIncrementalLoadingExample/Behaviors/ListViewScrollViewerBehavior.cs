using ISupportIncrementalLoadingExample.Util;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ISupportIncrementalLoadingExample.Behaviors
{
    public class ListViewScrollViewerBehavior : ScrollableBehaviorBase<ListViewBase>
    {
        public object ItemInView
        {
            get { return (object)GetValue(ItemInViewProperty); }
            set { SetValue(ItemInViewProperty, value); }
        }

        public static readonly DependencyProperty ItemInViewProperty =
            DependencyProperty.Register("ItemInView",
            typeof(object),
            typeof(ListViewScrollViewerBehavior),
            new PropertyMetadata(null, OnItemInViewPropertyChanged));

        private static void OnItemInViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ListViewScrollViewerBehavior;
            if (behavior != null && behavior.IsActive)
            {
                behavior.ItemInView = e.NewValue;
            }
        }

        protected override ScrollViewer GetScrollViewer()
        {
            return ControlHelper.FindChild<ScrollViewer>(AssociatedElement);
        }

        protected override void Scroll()
        {
            if (AssociatedElement != null && ItemInView != null)
            {
                var list = AssociatedElement.ItemsSource as IList;
                if (list != null && list.IndexOf(ItemInView) != -1)
                {
                    this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        if (ItemInView != null)
                        {
                            AssociatedElement.ScrollIntoView(ItemInView, ScrollIntoViewAlignment.Leading);
                            ItemInView = null;
                        }
                    });
                }
            }
            else
                base.Scroll();
        }
    }
}
