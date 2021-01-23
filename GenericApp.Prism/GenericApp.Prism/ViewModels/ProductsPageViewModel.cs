using GenericApp.Common.Responses;
using GenericApp.Common.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using GenericApp.Prism.ItemViewModels;

namespace GenericApp.Prism.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private string _search;
        private List<ProductResponse> _myProducts;
        private DelegateCommand _searchCommand;
        private ObservableCollection<ProductItemViewModel> _products;

        private int _cartNumber;
        public int CartNumber
        {
            get => _cartNumber;
            set => SetProperty(ref _cartNumber, value);
        }

        public ObservableCollection<ProductItemViewModel> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowProducts));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowProducts();
            }
        }

        public ProductsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Products";
            LoadProductsAsync();


        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }



        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error de conexión.", "Aceptar");
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<ProductResponse>(
                url,
                "api",
                "/Products");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            _myProducts = (List<ProductResponse>)response.Result;
            ShowProducts();

            //List<Product> myProducts = (List<Product>)response.Result;
            //Products = new ObservableCollection<Product>(myProducts);
        }

        private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p => new ProductItemViewModel(_navigationService)
                {
                    Category = p.Category,
                    Description = p.Description,
                    Id = p.Id,
                    IsActive = p.IsActive,
                    Name = p.Name,
                    Price = p.Price,
                    ProductImages = p.ProductImages,
                })
    .ToList());

            }
            else
            {
                Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p => new ProductItemViewModel(_navigationService)
                {
                    Category = p.Category,
                    Description = p.Description,
                    Id = p.Id,
                    IsActive = p.IsActive,
                    Name = p.Name,
                    Price = p.Price,
                    ProductImages = p.ProductImages,
                })
    .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
    .ToList());
            }
        }
    }
}