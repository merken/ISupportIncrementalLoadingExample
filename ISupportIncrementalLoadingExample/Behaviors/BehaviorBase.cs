using ISupportIncrementalLoadingExample.Util;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace ISupportIncrementalLoadingExample.Behaviors
{
    public abstract class BehaviorBase<T> : DependencyObject, IBehavior where T : FrameworkElement
    {
        private T element;

        /// <summary
        /// !Important!
        /// Whenever you define Dependency Properties on Behaviors (or any DependencyObject for that matter), you need to set a private static function to act on PropertyMetadata changed.
        /// Since this is static, this can be executed multiple times from DependencyObjects that are not part of the main UI thread anymore
        /// So whenever you want to change something on the UI thread or push changes of bindingexpressions to the source, you need to surround it with an if-check:
        /// if(IsActive){
        /// //your UI and bindingexpression code here
        /// }
        /// </summary>
        private bool isActive = false;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        public T AssociatedElement { get { return element; } }

        public DependencyObject AssociatedObject { get { return element; } }

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject as T == null)
                throw new NotSupportedException("The associated object must be of type " + typeof(T).Name);
            element = associatedObject as T;
            ElementAttached();
        }

        protected virtual void ElementAttached()
        {
            element.Loaded += ElementLoaded;
            isActive = true;
        }

        protected virtual void ElementLoaded(object sender, RoutedEventArgs e)
        {
            var page = ControlHelper.GetParentPage(element);
            if (page != null && page.NavigationCacheMode == NavigationCacheMode.Disabled)//Subcribe to the unloading event to handle state
                page.Unloaded += PageUnloaded;
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            this.Detach();
        }

        public void Detach()
        {
            ElementDetached();
            isActive = false;
        }

        protected virtual void ElementDetached()
        {
            var page = ControlHelper.GetParentPage(element);
            if (page != null && page.NavigationCacheMode == NavigationCacheMode.Disabled)//Subcribe to the unloading event to handle state
                page.Unloaded -= PageUnloaded;
            element.Loaded -= ElementLoaded;
        }
    }
}
