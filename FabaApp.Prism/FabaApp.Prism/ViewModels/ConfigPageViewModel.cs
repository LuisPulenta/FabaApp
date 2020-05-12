using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FabaApp.Prism.ViewModels
{
    public class ConfigPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ConfigPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Configuración";
            this._navigationService = navigationService;
        }
    }
}
