using System.Collections.ObjectModel;
using Newtonsoft.Json;
using GenericApp.Common.Helpers;
using GenericApp.Common.Responses;
using GenericApp.Prism.ItemViewModels;
using Prism.Navigation;

namespace GenericApp.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Generic App";
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
    }
}