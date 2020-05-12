using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using FabaApp.Common.Services;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabaApp.Prism.ViewModels
{
    public class AddRecipePageViewModel : ViewModelBase
    {
        private SocialWorkResponse _socialWork;
        private ObservableCollection<SocialWorkResponse> _socialWorks;
        private ObservableCollection<CodeItemViewModel> _codes;
        private ObservableCollection<CodeItemViewModel> _codes2;
                
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isRefreshing1;
        private bool _isRefreshing2;
        private int _cantRecipes;
        private UserResponse _user;
        private ImageSource _imageSource1;
        private ImageSource _imageSource2;
        private ImageSource _imageSource3;
        private ImageSource _imageSource4;
        private DelegateCommand _takePhotoCommand1;
        private DelegateCommand _takePhotoCommand2;
        private DelegateCommand _takePhotoCommand3;
        private DelegateCommand _takePhotoCommand4;
        private DelegateCommand _loadCodesCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _refresh1Command;
        private DelegateCommand _refresh2Command;
        
        private MediaFile _file1;
        private MediaFile _file2;
        private MediaFile _file3;
        private MediaFile _file4;

        private DateTime _recipeDate;
        

        private byte[] _imageArray1;
        private byte[] _imageArray2;
        private byte[] _imageArray3;
        private byte[] _imageArray4;

        private string _filter;
        private string _name;

        public DelegateCommand Refresh1Command => _refresh1Command ?? (_refresh1Command = new DelegateCommand(Refresh1));
        public DelegateCommand Refresh2Command => _refresh2Command ?? (_refresh2Command = new DelegateCommand(Refresh2));
        
        public DelegateCommand LoadCodesCommand => _loadCodesCommand ?? (_loadCodesCommand = new DelegateCommand(LoadCodes));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        public DelegateCommand TakePhotoCommand1 => _takePhotoCommand1 ?? (_takePhotoCommand1 = new DelegateCommand(TakePhoto1));
        public DelegateCommand TakePhotoCommand2 => _takePhotoCommand2 ?? (_takePhotoCommand1 = new DelegateCommand(TakePhoto2));
        public DelegateCommand TakePhotoCommand3 => _takePhotoCommand3 ?? (_takePhotoCommand1 = new DelegateCommand(TakePhoto3));
        public DelegateCommand TakePhotoCommand4 => _takePhotoCommand4 ?? (_takePhotoCommand1 = new DelegateCommand(TakePhoto4));
        public int NroFoto;
        public DateTime Hoy { get; set; }

        public DateTime RecipeDate
        {
            get => _recipeDate;
            set => SetProperty(ref _recipeDate, value);
        }

        public int CantRecipes
        {
            get => _cantRecipes;
            set => SetProperty(ref _cantRecipes, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }

        public MediaFile File1
        {
            get => _file1;
            set => SetProperty(ref _file1, value);
        }
        public MediaFile File2
        {
            get => _file2;
            set => SetProperty(ref _file2, value);
        }

        public MediaFile File3
        {
            get => _file3;
            set => SetProperty(ref _file3, value);
        }
        public MediaFile File4
        {
            get => _file4;
            set => SetProperty(ref _file4, value);
        }

        public ImageSource ImageSource1
        {
            get => _imageSource1;
            set => SetProperty(ref _imageSource1, value);
        }

        public ImageSource ImageSource2
        {
            get => _imageSource2;
            set => SetProperty(ref _imageSource2, value);
        }

        public ImageSource ImageSource3
        {
            get => _imageSource3;
            set => SetProperty(ref _imageSource3, value);
        }

        public ImageSource ImageSource4
        {
            get => _imageSource4;
            set => SetProperty(ref _imageSource4, value);
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

        public bool IsRefreshing1
        {
            get => _isRefreshing1;
            set => SetProperty(ref _isRefreshing1, value);
        }

        public bool IsRefreshing2
        {
            get => _isRefreshing2;
            set => SetProperty(ref _isRefreshing2, value);
        }

        public List<CodeItemViewModel> MyCodes { get; set; }


        public SocialWorkResponse SocialWork
        {
            get => _socialWork;
            set
            {
                if (_socialWork != value)
                {
                    SetProperty(ref _socialWork, value);
                }
            }
        }

        public ObservableCollection<SocialWorkResponse> SocialWorks
        {
            get => _socialWorks;
            set => SetProperty(ref _socialWorks, value);
        }

        public ObservableCollection<CodeItemViewModel> Codes
        {
            get => _codes;
            set => SetProperty(ref _codes, value);
        }

        public ObservableCollection<CodeItemViewModel> Codes2
        {
            get => _codes2;
            set => SetProperty(ref _codes2, value);
        }

        

        

        public AddRecipePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Hoy = DateTime.Today;
            RecipeDate = DateTime.Today;
            LoadSocialWorksAsync();
            Codes = new ObservableCollection<CodeItemViewModel>();
            Codes2 = new ObservableCollection<CodeItemViewModel>();
            instance = this;
            ImageSource1 = "noimage.png";
            ImageSource2 = "noimage.png";
            ImageSource3 = "noimage.png";
            ImageSource4 = "noimage.png";

            IsEnabled = true;
            Title = "Agregar Nueva Receta";

        }

        #region Singleton

        private static AddRecipePageViewModel instance;
        public static AddRecipePageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private async void LoadSocialWorksAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Revise su conexión a Internet",
                    "Aceptar");
                return;
            }

            Response response = await _apiService.GetListAsync<SocialWorkResponse>(url, "api", "/SocialWorks");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }
            List<SocialWorkResponse> list = (List<SocialWorkResponse>)response.Result;
            SocialWorks = new ObservableCollection<SocialWorkResponse>(list.OrderBy(t => t.Name));
        }

        private async void TakePhoto1()
        {
            NroFoto = 1;
            await _navigationService.NavigateAsync("TakePhotoPage");
        }

        private async void TakePhoto2()
        {
            NroFoto = 2;
            await _navigationService.NavigateAsync("TakePhotoPage");
        }

        private async void TakePhoto3()
        {
            NroFoto = 3;
            await _navigationService.NavigateAsync("TakePhotoPage");
        }

        private async void TakePhoto4()
        {
            NroFoto = 4;
            await _navigationService.NavigateAsync("TakePhotoPage");
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar el nombre de un Paciente.", "Aceptar");
                return;
            }

            if (SocialWork==null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar una Obra Social.", "Aceptar");
                return;
            }

            if (CantRecipes < 1)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe cargar al menos un ítem.", "Aceptar");
                return;
            }

            if (_file1 == null && _file2 == null && _file3 == null && _file4 == null)

            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe cargar al menos una foto.", "Aceptar");
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = true;
                IsEnabled = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Revise su conexión a Internet",
                    "Aceptar");
                return;
            }


            byte[] ImageArray1 = null;
            if (File1 != null)
            {
                ImageArray1 = FilesHelper.ReadFully(this.File1.GetStream());
                File1.Dispose();
            }

            byte[] ImageArray2 = null;
            if (File2 != null)
            {
                ImageArray2 = FilesHelper.ReadFully(this.File2.GetStream());
                File2.Dispose();
            }

            byte[] ImageArray3 = null;
            if (File3 != null)
            {
                ImageArray3 = FilesHelper.ReadFully(this.File3.GetStream());
                File3.Dispose();
            }

            byte[] ImageArray4 = null;
            if (File4 != null)
            {
                ImageArray4 = FilesHelper.ReadFully(this.File4.GetStream());
                File4.Dispose();
            }

            var user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var myrecipe = new RecipeRequest
            {
                DischargeDate=DateTime.Now,
                Flag1=true,
                Flag2 = true,
                Flag3 = true,
                Flag4 = true,
                PictureArray1= ImageArray1,
                PictureArray2 = ImageArray2,
                PictureArray3 = ImageArray3,
                PictureArray4 = ImageArray4,
                Name =Name,
                RecipeDate= RecipeDate,
                SocialWorkId=SocialWork.Id,
                State="Grabado",
                StateDate = DateTime.Now,
                UserId=user.Id
            };

            var response = await _apiService.PostAsync(
            url,
            "api",
            "/Recipes",
            myrecipe,
            "bearer",
            token.Token);

            

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }
            
            var myRecipe = (RecipeRequest)response.Result;

            foreach (var recipeDetail in Codes2)
            {
                var myRecipeDetail = new RecipeDetailRequest
                {
                    Code = recipeDetail.Code,
                    Description = recipeDetail.Description,
                    Quantity = recipeDetail.Qty,
                    RecipeId= myRecipe.Id,
                };
                var response2 = await _apiService.PostAsync(
                        url,
                        "api",
                        "/RecipeDetails",
                        myRecipeDetail,
                        "bearer",
                        token.Token);
                
                if (!response2.IsSuccess)
                {
                    IsRunning = false;
                    IsEnabled = true;

                    await App.Current.MainPage.DisplayAlert(
                        "Error",
                        response.Message,
                        "Aceptar");
                    return;
                }
            }
            IsRunning = false;
            IsEnabled = true;


            await App.Current.MainPage.DisplayAlert(
                "Ok",
                "Guardada con éxito!!",
                "Aceptar");

            //Refrescar Lista de Recetas

            await _navigationService.GoBackAsync();
        }

        public async void LoadCodes()
        {

            if (SocialWork==null)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe seleccionar una Obra Social",
                    "Aceptar");
                return;
            }
            IsRefreshing1 = true;
            _user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            var url = App.Current.Resources["UrlAPI"].ToString();
            var controller = $"/Codes/{SocialWork.Id}";



            Response response = await _apiService.GetListAsync<CodeItemViewModel>(url, "api", controller);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema para recuperar datos.", "Aceptar");
                return;
            }
            MyCodes = (List<CodeItemViewModel>)response.Result;
            RefreshList();
            IsRefreshing1 = false;
        }


        public void RefreshList()
        {
            IsRefreshing1 = true;
            if (string.IsNullOrEmpty(this.Filter))
            {

                var myListAddItemViewModel = MyCodes.Select(a => new CodeItemViewModel(_navigationService)
                {
                    Active = a.Active,
                    Code = a.Code,
                    Description = a.Description,
                    Id = a.Id,
                    SocialWork = a.SocialWork,
                });
                Codes = new ObservableCollection<CodeItemViewModel>(myListAddItemViewModel
                    .OrderBy(o => o.Code));
            }
            else
            {
                var myListAddItemViewModel = MyCodes.Select(a => new CodeItemViewModel(_navigationService)
                {
                    Active = a.Active,
                    Code = a.Code,
                    Description = a.Description,
                    Id = a.Id,
                    SocialWork = a.SocialWork,
                });

                Codes = new ObservableCollection<CodeItemViewModel>
                    (
                        myListAddItemViewModel
                        .OrderBy(o => o.Code)
                    .Where(
                            o => (o.Code.ToLower().Contains(Filter.ToLower()))
                            ||
                            (o.Description.ToLower().Contains(Filter.ToLower()))
                            ));
            }
            IsRefreshing1 = false;
        }

       public async void Refresh1()
        {
            IsRefreshing1 = true;
            Codes.OrderBy(t => t.Code);
            IsRefreshing1 = false;
        }

        public async void Refresh2()
        {
            IsRefreshing2 = true;

            var myListCodeItemViewModel = Codes2.Select(a => new CodeItemViewModel(_navigationService)
            {
                Active=a.Active,
                Code=a.Code,
                Description=a.Description,
                Id=a.Id,
                Qty=a.Qty,
                SocialWork=a.SocialWork,
            });
            Codes2 = new ObservableCollection<CodeItemViewModel>(myListCodeItemViewModel
                   .OrderBy(o => o.Code));
            CantRecipes = Codes2.Count();
            IsRefreshing2 = false;
        }
    }
}


