using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using FabaApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FabaApp.Prism.ViewModels
{
    

    public class RecipesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private UserResponse _user;
        private RecipeResponse _recipe;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isRefreshing;
        private ObservableCollection<RecipeItemViewModel> _recipes;
        private static RecipesPageViewModel _instance;
        private int _cantRecipes;
        private string _filter;
        private DelegateCommand _searchCommand;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _addRecipeCommand;

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }

        public RecipeResponse Recipe
        {
            get => _recipe;
            set => SetProperty(ref _recipe, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ObservableCollection<RecipeItemViewModel> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }

        public int CantRecipes
        {
            get => _cantRecipes;
            set => SetProperty(ref _cantRecipes, value);
        }

        public static RecipesPageViewModel GetInstance()
        {
            return _instance;
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));
        public DelegateCommand AddRecipeCommand => _addRecipeCommand ?? (_addRecipeCommand = new DelegateCommand(AddRecipe));
        

        public List<RecipeItemViewModel> MyRecipes { get; set; }

        public RecipesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _instance = this;
            LoadRecipes();
            Recipes = new ObservableCollection<RecipeItemViewModel>();
            Title = "Recetas";
            IsRefreshing = false;
        }

        public async void LoadRecipes()
        {
            IsRefreshing = true;
            _user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var url = App.Current.Resources["UrlAPI"].ToString();
            var controller = $"/Recipes/{_user.Id}";

            

            Response response = await _apiService.GetListAsync<RecipeItemViewModel>(url, "api", controller, "bearer", token.Token);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                IsRefreshing = false;
                return;
            }
            MyRecipes = (List<RecipeItemViewModel>)response.Result;
            IsRefreshing = false;
            RefreshList();
            
        }

        public void RefreshList()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(this.Filter))
            {

                var myListRecipesViewModel = MyRecipes.Select(a => new RecipeItemViewModel(_navigationService, _apiService)
                {
                    Id = a.Id,
                    DischargeDate=a.DischargeDate,
                    Flag1=a.Flag1,
                    Flag2 = a.Flag2,
                    Flag3 = a.Flag3,
                    Flag4 = a.Flag4,
                    Foto1=a.Foto1,
                    Foto2 = a.Foto2,
                    Foto3 = a.Foto3,
                    Foto4 = a.Foto4,
                    Name=a.Name,
                    RecipeDate=a.RecipeDate,
                    SocialWorkId=a.SocialWorkId,
                    State=a.State,
                    StateDate=a.StateDate,
                    UserId=a.UserId,
                    SocialWork = a.SocialWork,
                    RecipeDetails = a.RecipeDetails,
                    CantItems = a.RecipeDetails.Count(),
                    CantFotos=QtyPhoto(a.Foto1, a.Foto2, a.Foto3, a.Foto4)
                });
                Recipes = new ObservableCollection<RecipeItemViewModel>(myListRecipesViewModel
                    .OrderBy(o => o.RecipeDate));

                CantRecipes = Recipes.Count();
                
            }
            else
            {
                var myListRecipesViewModel = MyRecipes.Select(a => new RecipeItemViewModel(_navigationService, _apiService)
                {
                    Id = a.Id,
                    DischargeDate = a.DischargeDate,
                    Flag1 = a.Flag1,
                    Flag2 = a.Flag2,
                    Flag3 = a.Flag3,
                    Flag4 = a.Flag4,
                    Foto1 = a.Foto1,
                    Foto2 = a.Foto2,
                    Foto3 = a.Foto3,
                    Foto4 = a.Foto4,
                    Name = a.Name,
                    RecipeDate = a.RecipeDate,
                    SocialWorkId = a.SocialWorkId,
                    State = a.State,
                    StateDate = a.StateDate,
                    UserId = a.UserId,
                    SocialWork=a.SocialWork,
                    RecipeDetails=a.RecipeDetails,
                    CantItems= a.RecipeDetails.Count(),
                    CantFotos = QtyPhoto(a.Foto1, a.Foto2, a.Foto3, a.Foto4)
                });

                Recipes = new ObservableCollection<RecipeItemViewModel>
                    (
                        myListRecipesViewModel
                        .OrderBy(o => o.RecipeDate)
                    .Where(
                            o => (o.Name.ToLower().Contains(Filter.ToLower()))
                            ||
                            (o.SocialWork.Name.ToLower().Contains(Filter.ToLower()))
                            ));
                CantRecipes = Recipes.Count();
            }
            IsRefreshing = false;
        }

        private async void Search()
        {
            RefreshList();
        }

        private async void Refresh()
        {
            LoadRecipes();
        }

        static int QtyPhoto(string Foto1, string Foto2, string Foto3, string Foto4)
        {
            int total = 0;
            int F1 = 0;
            int F2 = 0;
            int F3 = 0;
            int F4 = 0;
            if (!string.IsNullOrEmpty(Foto1)) {F1 = 1;};
            if (!string.IsNullOrEmpty(Foto2)) { F2 = 1; };
            if (!string.IsNullOrEmpty(Foto3)) { F3 = 1; };
            if (!string.IsNullOrEmpty(Foto4)) { F4 = 1; };

            total = F1 + F2 + F3 + F4;
            return total;
        }

        private async void AddRecipe()
        {
            await _navigationService.NavigateAsync("AddRecipePage");
            //await _navigationService.NavigateAsync("/FabaAppMasterDetailPage/NavigationPage/AddRecipePage");
        }
    }
}