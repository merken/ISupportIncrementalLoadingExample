using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ISupportIncrementalLoadingExample.Incremental;
using ISupportIncrementalLoadingExample.Incremental.Sources;
using ISupportIncrementalLoadingExample.Model;
using ISupportIncrementalLoadingExample.Scrollable;
using ISupportIncrementalLoadingExample.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading.Tasks;

namespace ISupportIncrementalLoadingExample.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly FooService fooService;
        private FooSource fooSource;
        private GroupedFooSource groupedFooSource;

        private bool noMoreFoos;
        public bool NoMoreFoos
        {
            get
            {
                return noMoreFoos;
            }
            set
            {
                this.noMoreFoos = value;
                RaisePropertyChanged();
            }
        }

        private bool noMoreGroupedFoos;
        public bool NoMoreGroupedFoos
        {
            get
            {
                return noMoreGroupedFoos;
            }
            set
            {
                this.noMoreGroupedFoos = value;
                RaisePropertyChanged();
            }
        }

        private IncrementalCollection<FooSource, Foo> foos;

        public IncrementalCollection<FooSource, Foo> Foos
        {
            get { return foos; }
            set
            {
                foos = value;
                RaisePropertyChanged();
            }
        }

        private IncrementalCollection<GroupedFooSource, GroupModel<String, Foo>> groupedFoos;

        public IncrementalCollection<GroupedFooSource, GroupModel<String, Foo>> GroupedFoos
        {
            get { return groupedFoos; }
            set
            {
                groupedFoos = value;
                RaisePropertyChanged();
            }
        }

        private ScrollableObservableCollection<Foo> scrollableFoos;

        public ScrollableObservableCollection<Foo> ScrollableFoos
        {
            get { return scrollableFoos; }
            set
            {
                scrollableFoos = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ClearCommand { get; set; }
        public RelayCommand AboutCommand { get; set; }

        public MainViewModel()
        {
            fooService = new FooService();
            ClearCommand = new RelayCommand(() =>
            {
                this.Foos.Clear();
                this.GroupedFoos.Clear();
            });

            AboutCommand = new RelayCommand(() =>
            {
                var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();

                navigationService.Navigate(typeof(AboutPage), null);
            });
        }

        public override async void Initialize(object param)
        {
            if (!IsLoaded)
            {
                InitializeFooSource();
                InitializeGroupedFooSource();
                await InitializeScrollableFoosAsync();
                IsLoaded = true;
            }
        }

        private void InitializeFooSource()
        {
            NoMoreFoos = false;
            fooSource = new FooSource(async (currentPage, itemsPerPage) =>
            {
                FooResponse response = null;
                try
                {
                    IsLoading = true;
                    response = await fooService.GetFoos(currentPage, itemsPerPage);
                }
                catch (Exception ex)//Something completely unexpected happened, write a debug line to ensure persistence of the error
                {
                    //TODO ERROR HANDLING
                }
                finally
                {
                    IsLoading = false;//Hide the progressbar
                }
                return response;
            });

            this.Foos = new IncrementalCollection<FooSource, Foo>(fooSource);
            this.Foos.HasMoreItemsChanged += (s, hasm) =>
            {
                this.NoMoreFoos = !hasm.HasMoreItems;
            };
        }

        private void InitializeGroupedFooSource()
        {
            NoMoreGroupedFoos = false;
            groupedFooSource = new GroupedFooSource(async (currentPage, itemsPerPage) =>
            {
                GroupedFooResponse response = null;
                try
                {
                    IsLoading = true;
                    response = await fooService.GetGroupedFoos(currentPage, itemsPerPage);
                }
                catch (Exception ex)//Something completely unexpected happened, write a debug line to ensure persistence of the error
                {
                    //TODO ERROR HANDLING
                }
                finally
                {
                    IsLoading = false;//Hide the progressbar
                }
                return response;
            });

            this.GroupedFoos = new IncrementalCollection<GroupedFooSource, GroupModel<string, Foo>>(groupedFooSource);
            this.GroupedFoos.HasMoreItemsChanged += (s, hasm) =>
            {
                this.NoMoreGroupedFoos = !hasm.HasMoreItems;
            };
        }

        private async Task InitializeScrollableFoosAsync()
        {
            try
            {
                IsLoading = true;
                var response = await fooService.GetFoos(0, 999);
                ScrollableFoos = new ScrollableObservableCollection<Foo>(response.Foos);
            }
            catch (Exception ex)//Something completely unexpected happened, write a debug line to ensure persistence of the error
            {
                //TODO ERROR HANDLING
            }
        }
        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }
    }
}