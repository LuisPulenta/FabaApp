using FabaApp.Common.Helpers;
using FabaApp.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace FabaApp.Prism.ViewModels
{
    public class CodeItemViewModel : CodeResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _addItemCommand;
        private DelegateCommand _deleteItemCommand;
        private DelegateCommand _addOneCommand;
        private DelegateCommand _removeOneCommand;

        public CodeItemViewModel(INavigationService navigationService)
        {
            //_navigationService = navigationService;
        }
        public DelegateCommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new DelegateCommand(AddItem));
        public DelegateCommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new DelegateCommand(DeleteItem));
        public DelegateCommand AddOneCommand => _addOneCommand ?? (_addOneCommand = new DelegateCommand(AddOne));
        public DelegateCommand RemoveOneCommand => _removeOneCommand ?? (_removeOneCommand = new DelegateCommand(RemoveOne));

        private async void AddItem()
        {
            AddRecipePageViewModel addRecipePageViewModel = AddRecipePageViewModel.GetInstance();
            this.Qty = 1;
            bool Bandera = true;
            foreach (var code in addRecipePageViewModel.Codes2)
            {
                if(code.Code == this.Code)
                {
                    Bandera = false;
                }
            }
            if (Bandera)
            {
                addRecipePageViewModel.Codes2.Add(this);
                addRecipePageViewModel.Refresh2();
            }
           
        }

        private async void DeleteItem()
        {
            AddRecipePageViewModel addRecipePageViewModel = AddRecipePageViewModel.GetInstance();
            addRecipePageViewModel.Codes2.Remove(this);
            addRecipePageViewModel.Refresh2();
        }

        private async void AddOne()
        {
            AddRecipePageViewModel addRecipePageViewModel = AddRecipePageViewModel.GetInstance();
            addRecipePageViewModel.Codes2.Remove(this);
            this.Qty = this.Qty + 1;
            addRecipePageViewModel.Codes2.Add(this);
            addRecipePageViewModel.Refresh2();
        }

        private async void RemoveOne()
        {
            if (this.Qty>1)
            {
                AddRecipePageViewModel addRecipePageViewModel = AddRecipePageViewModel.GetInstance();
                addRecipePageViewModel.Codes2.Remove(this);
                this.Qty = this.Qty - 1;
                addRecipePageViewModel.Codes2.Add(this);
                addRecipePageViewModel.Refresh2();
            }
        }
    }
}