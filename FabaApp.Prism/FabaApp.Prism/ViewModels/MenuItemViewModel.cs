using FabaApp.Common.Models;
using FabaApp.Common.Services;
using Prism.Commands;
using Prism.Navigation;

namespace FabaApp.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService, IApiService apiService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync($"/FabaAppMasterDetailPage/NavigationPage/{PageName}");
        }
    }
}
