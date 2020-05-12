using Prism;
using Prism.Ioc;
using FabaApp.Prism.ViewModels;
using FabaApp.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FabaApp.Common.Services;
using FabaApp.Common.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FabaApp.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY2MzIyQDMxMzcyZTMzMmUzMFVnNW5KSnM2dTZmRDljWm1RYTduQXFwRmNKSzVPWk1lT1JGSFRySXZCUTA9");
            InitializeComponent();

            if (Settings.IsLogin)
            {
                await NavigationService.NavigateAsync("/FabaAppMasterDetailPage/NavigationPage/RecipesPage");
            }

            else
            {
                await NavigationService.NavigateAsync("/FabaAppMasterDetailPage/NavigationPage/LoginPage");
                //await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<FabaAppMasterDetailPage, FabaAppMasterDetailPageViewModel>();


            containerRegistry.RegisterForNavigation<RecipesPage, RecipesPageViewModel>();

            containerRegistry.RegisterForNavigation<WrongRecipesPage, WrongRecipesPageViewModel>();
            containerRegistry.RegisterForNavigation<AddRecipePage, AddRecipePageViewModel>();

            containerRegistry.RegisterForNavigation<TakePhotoPage, TakePhotoPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfigPage, ConfigPageViewModel>();
            containerRegistry.RegisterForNavigation<EditRecipePage, EditRecipePageViewModel>();
        }
    }
}
