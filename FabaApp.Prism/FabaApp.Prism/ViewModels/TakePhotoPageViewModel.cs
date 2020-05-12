using FabaApp.Common.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;


namespace FabaApp.Prism.ViewModels
{
    public class TakePhotoPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private Picture orderPicture;
        

        private bool _isRunning;
        private bool _isEnabled;
        private string _sourcePage;

        private ImageSource _imageSource;
        private MediaFile _file;


        public string SourcePage
        {
            get => _sourcePage;
            set => SetProperty(ref _sourcePage, value);
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

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private DelegateCommand _cancelCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _takePhotoCommand;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        public DelegateCommand TakePhotoCommand => _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(TakePhoto));

        public TakePhotoPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Tomar Fotografía";
            IsEnabled = true;
            ImageSource = "noimage";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            SourcePage = parameters.GetValue<string>("sourcePage");
            var a = 1;
        }


        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }



        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            
            

            
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            
            
            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
               



            }
            IsRunning = false;
        }


        private async void Save()
        {

            if (SourcePage=="Add")
            {
                var addRecipePageViewModel = AddRecipePageViewModel.GetInstance();

                if (addRecipePageViewModel.NroFoto == 1)
                {

                    if (_file == null)
                    {
                        addRecipePageViewModel.ImageSource1 = "noimage";
                    }
                    else
                    {
                        addRecipePageViewModel.ImageSource1 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        addRecipePageViewModel.File1 = _file;
                    }
                }

                if (addRecipePageViewModel.NroFoto == 2)
                {

                    if (_file == null)
                    {
                        addRecipePageViewModel.ImageSource2 = "noimage";
                    }
                    else
                    {
                        addRecipePageViewModel.ImageSource2 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        addRecipePageViewModel.File2 = _file;
                    }
                }

                if (addRecipePageViewModel.NroFoto == 3)
                {

                    if (_file == null)
                    {
                        addRecipePageViewModel.ImageSource3 = "noimage";
                    }
                    else
                    {
                        addRecipePageViewModel.ImageSource3 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        addRecipePageViewModel.File3 = _file;
                    }
                }

                if (addRecipePageViewModel.NroFoto == 4)
                {

                    if (_file == null)
                    {
                        addRecipePageViewModel.ImageSource4 = "noimage";
                    }
                    else
                    {
                        addRecipePageViewModel.ImageSource4 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        addRecipePageViewModel.File4 = _file;
                    }
                }
                await _navigationService.GoBackAsync();
            }


            if (SourcePage == "Edit")
            {
                var editRecipePageViewModel = EditRecipePageViewModel.GetInstance();

                if (editRecipePageViewModel.NroFoto == 1)
                {

                    if (_file == null)
                    {
                        editRecipePageViewModel.ImageSource1 = "noimage";
                    }
                    else
                    {
                        editRecipePageViewModel.ImageSource1 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        editRecipePageViewModel.File1 = _file;
                    }
                }

                if (editRecipePageViewModel.NroFoto == 2)
                {

                    if (_file == null)
                    {
                        editRecipePageViewModel.ImageSource2 = "noimage";
                    }
                    else
                    {
                        editRecipePageViewModel.ImageSource2 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        editRecipePageViewModel.File2 = _file;
                    }
                }

                if (editRecipePageViewModel.NroFoto == 3)
                {

                    if (_file == null)
                    {
                        editRecipePageViewModel.ImageSource3 = "noimage";
                    }
                    else
                    {
                        editRecipePageViewModel.ImageSource3 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        editRecipePageViewModel.File3 = _file;
                    }
                }

                if (editRecipePageViewModel.NroFoto == 4)
                {

                    if (_file == null)
                    {
                        editRecipePageViewModel.ImageSource4 = "noimage";
                    }
                    else
                    {
                        editRecipePageViewModel.ImageSource4 = ImageSource.FromStream(() =>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        });
                        editRecipePageViewModel.File4 = _file;
                    }
                }
                await _navigationService.GoBackAsync();
            }

        }
    }
}