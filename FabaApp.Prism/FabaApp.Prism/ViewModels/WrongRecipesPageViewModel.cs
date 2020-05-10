using Prism.Navigation;

namespace FabaApp.Prism.ViewModels
{
    public class WrongRecipesPageViewModel : ViewModelBase
    {

        public WrongRecipesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Recetas Rechazadas";
        }
    }
}