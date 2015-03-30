using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ISupportIncrementalLoadingExample.Services
{
    public class NavigationService 
    {
        public bool CanGoBack
        {
            get { return frame != null && frame.CanGoBack; }
        }

        public void GoBack()
        {
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
            }
            else if (frame != null && frame.BackStackDepth == 0)
                CloseApp();
        }

        public void CloseApp()
        {
            Application.Current.Exit();
        }

        public void Navigate(Type type,object obj)
        {
            frame.Navigate(type, obj);
        }

        private Frame frame;

        public Frame RootFrame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
            }
        }
    }
}
