using Newtonsoft.Json;
using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace GenericApp.Prism.ItemViewModels
{
    public class ProductItemViewModel : ProductResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectProductCommand;
        

        public float Quantity { get; set; }

        public string Remarks { get; set; }

        public decimal Value => (decimal)Quantity * Price;


        public ProductItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand(SelectProductAsync));

        private async void SelectProductAsync()
        {
            NavigationParameters parameters = new NavigationParameters
                {
                    { "product", this }
                };
            Settings.Product = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(ProductsPage), parameters);
        }
    }
}