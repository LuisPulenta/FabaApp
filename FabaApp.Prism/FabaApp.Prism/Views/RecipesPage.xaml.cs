using FabaApp.Prism.ViewModels;
using Xamarin.Forms;

namespace FabaApp.Prism.Views
{
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var recipesPage = RecipesPageViewModel.GetInstance();
            recipesPage.RefreshList();
        }
    }
}
