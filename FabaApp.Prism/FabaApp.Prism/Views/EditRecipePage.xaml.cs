using FabaApp.Prism.ViewModels;
using Xamarin.Forms;

namespace FabaApp.Prism.Views
{
    public partial class EditRecipePage : ContentPage
    {
        public EditRecipePage()
        {
            InitializeComponent();
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var editRecipePage = EditRecipePageViewModel.GetInstance();
            editRecipePage.RefreshList();
        }
    }
}
