using CarpMc.MVVM.Model;
using CarpMc.MVVM.View;
using CarpMc.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarpMc.MVVM.ViewModel
{
    partial class ProfileViewModel : ObservableObject
    {
        private Sqlite sqlite = new Sqlite();

        [ObservableProperty]
        private string? _username;
        //public string? Username
        //{
        //    get { return _username; }
        //    set
        //    {
        //        _username = value;
        //        OnPropertyChanged(nameof(Username));
        //    }
        //}

        [ObservableProperty]
        private bool _isLoggedIn;
        //public bool IsLoggedIn
        //{
        //    get { return _isLoggedIn; }
        //    set
        //    {
        //        _isLoggedIn = value;
        //        OnPropertyChanged(nameof(IsLoggedIn));
        //    }
        //}

        public ProfileViewModel()
        {
            Username = ProfileModel.Username ?? "Not Logged In";
            IsLoggedIn = ProfileModel.IsLoggedIn ? ProfileModel.IsLoggedIn : false;
        }
    }
}
