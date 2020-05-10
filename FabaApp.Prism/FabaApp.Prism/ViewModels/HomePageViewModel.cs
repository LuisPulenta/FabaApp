using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace FabaApp.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;


        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            _navigationService = navigationService;
            Title = "FABA";
        }
    }
}
