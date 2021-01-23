using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using GenericApp.Common.Helpers;
using GenericApp.Common.Models;
using GenericApp.Common.Responses;
using GenericApp.Prism.ItemViewModels;
using GenericApp.Prism.Views;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class GenericAppMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private static GenericAppMasterDetailPageViewModel _instance;
        public static GenericAppMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }


        private UserResponse _user;
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        public GenericAppMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }


        public void LoadUser()
        {
            if (Settings.IsLogin)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                User = token.User;
            }
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                Icon = "ic_home",
                PageName = $"{nameof(HomePage)}",
                Title = "Inicio"
            },


                new Menu
            {
                Icon = "ic_card_giftcard",
                PageName = $"{nameof(ProductsPage)}",
                Title = "Productos"
            },
                       
                new Menu
            {
                Icon = "ic_person",
                PageName = $"{nameof(ModifyUserPage)}",
                Title = "Modificar Usuario",
                IsLoginRequired = true
            },
           
            new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = $"{nameof(LoginPage)}",
                Title = Settings.IsLogin ? "Cerrar Sesión" : "Login"
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}