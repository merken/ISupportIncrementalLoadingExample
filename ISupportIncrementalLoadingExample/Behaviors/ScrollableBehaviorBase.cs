using ISupportIncrementalLoadingExample.Scrollable;
using ISupportIncrementalLoadingExample.Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ISupportIncrementalLoadingExample.Behaviors
{
    public abstract class ScrollableBehaviorBase<T> : BehaviorBase<T>
       where T : FrameworkElement
    {
        protected ScrollViewer scrollViewer;

        protected abstract ScrollViewer GetScrollViewer();

        public IScrollable Scrollable
        {
            get { return (IScrollable)GetValue(ScrollableProperty); }
            set { SetValue(ScrollableProperty, value); }
        }

        public static readonly DependencyProperty ScrollableProperty =
            DependencyProperty.Register("Scrollable",
            typeof(object),
            typeof(ScrollableBehaviorBase<T>),
            new PropertyMetadata(null, OnScrollablePropertyChanged));

        private static void OnScrollablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IScrollable scrollable = (IScrollable)e.NewValue;
            var behavior = d as ScrollableBehaviorBase<T>;
            if (behavior != null && behavior.IsActive)
            {
                behavior.Scrollable = scrollable;
                behavior.Scroll();
            }
        }

        protected override void ElementLoaded(object sender, RoutedEventArgs e)
        {
            base.ElementLoaded(sender, e);
            scrollViewer = GetScrollViewer();
            scrollViewer.ViewChanged += ViewChanged;

            Scroll();
        }

        private void ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Scrollable != null)
            {
                Scrollable.VerticalOffset = (float)scrollViewer.VerticalOffset;
                Scrollable.HorizontalOffset = (float)scrollViewer.HorizontalOffset;
                Scrollable.ZoomFactor = scrollViewer.ZoomFactor;
            }
        }

        protected override void ElementDetached()
        {
            scrollViewer = ControlHelper.FindChild<ScrollViewer>(AssociatedElement);
            scrollViewer.ViewChanged -= ViewChanged;
            scrollViewer = null;
            base.ElementDetached();
        }

        protected virtual void Scroll()
        {
            if (Scrollable != null && scrollViewer != null)
                scrollViewer.ChangeView(Scrollable.HorizontalOffset, Scrollable.VerticalOffset, Scrollable.ZoomFactor);
        }
    }
}
