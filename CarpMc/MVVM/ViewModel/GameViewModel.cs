using CarpMc.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CarpMc.MVVM.ViewModel
{
    partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string>? _gameList;

        public GameViewModel()
        {
            GameList = Versions.VersionList;
        }
    }
}
