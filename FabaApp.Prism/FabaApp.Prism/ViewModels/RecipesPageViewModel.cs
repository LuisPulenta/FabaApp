using Prism.Commands;
using Prism.Navigation;

namespace FabaApp.Prism.ViewModels
{
    

    public class RecipesPageViewModel : ViewModelBase
    {

        private DelegateCommand _addRecipeCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand AddRecipeCommand => _addRecipeCommand ?? (_addRecipeCommand = new DelegateCommand(AddRecipe));

        public RecipesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Recetas";
        }

        private async void AddRecipe()
        {

            await _navigationService.NavigateAsync("AddRecipePage");
        }
    }
}