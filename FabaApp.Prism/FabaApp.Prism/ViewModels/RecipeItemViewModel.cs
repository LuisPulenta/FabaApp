using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using FabaApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabaApp.Prism.ViewModels
{
    public class RecipeItemViewModel: RecipeResponse
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _deleteRecipeCommand;
        public DelegateCommand DeleteRecipeCommand => _deleteRecipeCommand ?? (_deleteRecipeCommand = new DelegateCommand(DeleteRecipe));

        private DelegateCommand _editRecipeCommand;
        public DelegateCommand EditRecipeCommand => _editRecipeCommand ?? (_editRecipeCommand = new DelegateCommand(EditRecipe));

        public RecipeItemViewModel(INavigationService navigationService,IApiService apiService)
        {
            _navigationService = navigationService;
            this._apiService = apiService;
        }

        

        private async void DeleteRecipe()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
               "Confirmar",
               "¿Está seguro de borrar esta Receta?",
               "Si",
               "No");

            if (!answer)
            {
                return;
            }

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);


            foreach (RecipeDetailResponse recipeDetail in this.RecipeDetails)
            {
                var responseDetail = await _apiService.DeleteAsync(
                url,
                "api",
                "/RecipeDetails",
                recipeDetail.Id,
                "bearer",
                token.Token);
            }


            var response = await _apiService.DeleteAsync(
               url,
               "api",
               "/Recipes",
               this.Id,
               "bearer",
               token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se puedo borrar", //response.Message,
                    "Accept");
                return;
            }
            RecipesPageViewModel.GetInstance().LoadRecipes();
        }

        private async void EditRecipe()
        {
            Settings.Recipe = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("EditRecipePage");
        }
    }
}
