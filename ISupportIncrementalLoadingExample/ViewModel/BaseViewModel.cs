using GalaSoft.MvvmLight;

namespace ISupportIncrementalLoadingExample.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool isLoading;

        public bool IsLoading
        {
            get
            { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged();
            }
        }

        private bool isLoaded;

        public bool IsLoaded
        {
            get
            { return isLoaded; }
            set
            {
                isLoaded = value;
                RaisePropertyChanged();
            }
        }

        public abstract void Initialize(object param);

        public abstract void GoBack();
    }
}
