using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using FabaApp.Common.Services;
using FabaApp.Prism.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FabaApp.Prism.ViewModels;
using FabaApp.Prism;

namespace FabaApp.Prism.ViewModels
{
    public class FabaAppMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private UserResponse _user;
        private static FabaAppMasterDetailPageViewModel _instance;

        public FabaAppMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            LoadUser();
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }



        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_add_to_home_screen.png",
                    PageName = "RecipesPage",
                    Title = "Agregar Recetas",
                    IsLoginRequired = true
                },

                new Menu
                {
                    Icon = "ic_error.png",
                    PageName = "WrongRecipesPage",
                    Title = "Recetas Rechazadas",
                    IsLoginRequired = true
                },

                new Menu
                {
                    Icon = "",
                    PageName = "",
                    Title = ""
                },

                new Menu
                {
                    Icon = "ic_exit_to_app.png",
                    PageName = "LoginPage",
                    Title = "Cerrar Sesión"
                },

            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService, _apiService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }


        public static FabaAppMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);

            LoadUser();
        }

    }
}