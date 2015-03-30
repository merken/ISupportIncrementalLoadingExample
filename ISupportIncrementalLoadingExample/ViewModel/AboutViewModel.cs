using ISupportIncrementalLoadingExample.Services;
using Microsoft.Practices.ServiceLocation;
namespace ISupportIncrementalLoadingExample.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public override void Initialize(object param)
        {
            //Nothing to do here
        }

        public override void GoBack()
        {
            var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
            navigationService.GoBack();
        }
    }
}
