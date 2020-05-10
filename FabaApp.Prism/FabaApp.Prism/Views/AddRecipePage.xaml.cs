using FabaApp.Prism.ViewModels;
using Xamarin.Forms;

namespace FabaApp.Prism.Views
{
    public partial class AddRecipePage : ContentPage
    {
        public AddRecipePage()
        {
            InitializeComponent();
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var addRecipePage = AddRecipePageViewModel.GetInstance();
            addRecipePage.RefreshList();
        }
    }
}
