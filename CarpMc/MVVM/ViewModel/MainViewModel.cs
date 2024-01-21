using CarpMc.Core;
using CarpMc.MVVM.Model;
using ProjBobcat.Class.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CarpMc.MVVM.ViewModel
{
    class MainViewModel : Core.ViewModel
    {

        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }

        public ObservableCollection<MenuButton> MenuButtons { get; set; }

        private List<VersionInfo>? _versions;
        public List<VersionInfo>? Versions
        {
            get { return _versions; }
            set
            {
                _versions = value;
                OnPropertyChanged(nameof(Versions));
            }
        }

        private object? _currentView;
        public object? CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            SettingsVM = new SettingsViewModel();
            ProfileVM = new ProfileViewModel();

            CurrentView = HomeVM;

            Versions = Utils.Core.InitLauncherCore().VersionLocator.GetAllGames().ToList();

            MenuButtons = new ObservableCollection<MenuButton>
            {
                new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = true },
                new MenuButton { Name = "FaceWomanProfile", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false },
                new MenuButton { Name = "ApplicationSettings", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false },
            };
        }

        private void NavigateToHome(object obj)
        {
            CurrentView = HomeVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = true });
            MenuButtons.Add(new MenuButton { Name = "FaceWomanProfile", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "ApplicationSettings", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false });
        }

        private void NavigateToProfile(object obj)
        {
            CurrentView = ProfileVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "FaceWomanProfile", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = true });
            MenuButtons.Add(new MenuButton { Name = "ApplicationSettings", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false });
        }

        private void NavigateToSettings(object obj)
        {
            CurrentView = SettingsVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "FaceWomanProfile", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "ApplicationSettings", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = true });
        }
    }
}
