using FabaApp.Common.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabaApp.Prism.ViewModels
{
    public class RecipeItemViewModel: RecipeResponse
    {
        private readonly INavigationService _navigationService;

        public RecipeItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
        }
    }
}
