using CarpMc.Core;
using CarpMc.MVVM.Model;
using CarpMc.MVVM.View;
using CarpMc.Utils;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace CarpMc.MVVM.ViewModel
{
    public partial class ProfileViewModel : Core.ViewModel
    {
        private Sqlite sqlite = new Sqlite();


        private string? _username;
        public string? Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public ProfileViewModel()
        {
            Username = ProfileModel.Username ?? "Not Logged In";
            IsLoggedIn = ProfileModel.IsLoggedIn ? ProfileModel.IsLoggedIn : false;
        }
    }
}
