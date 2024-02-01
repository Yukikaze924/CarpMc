using CarpMc.Core;
using CarpMc.MVVM.Model;
using CarpMc.Utils;
using ProjBobcat.Class.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace CarpMc.MVVM.ViewModel
{
    partial class MainViewModel : Core.ViewModel
    {

        public HomeViewModel HomeVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public DownloadViewModel DownloadVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        public ObservableCollection<MenuButton> MenuButtons { get; set; }

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
            ProfileVM = new ProfileViewModel();
            DownloadVM = new DownloadViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            MenuButtons = new ObservableCollection<MenuButton>
            {
                new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = true },
                new MenuButton { Name = "Account", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false },
                new MenuButton { Name = "Download", NavigateCommand = new RelayCommand(NavigateToDownload), isSelected = false },
                new MenuButton { Name = "Cog", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false },
            };

            Versions.VersionList = new ObservableCollection<string>(Utils.Core.InitLauncherCore().VersionLocator.GetAllGames().ToList().Select(v => v.Id).ToList());
        }

        private void NavigateToHome(object obj)
        {
            CurrentView = HomeVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = true });
            MenuButtons.Add(new MenuButton { Name = "Account", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Download", NavigateCommand = new RelayCommand(NavigateToDownload), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Cog", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false });
        }

        private void NavigateToProfile(object obj)
        {
            CurrentView = ProfileVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Account", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = true });
            MenuButtons.Add(new MenuButton { Name = "Download", NavigateCommand = new RelayCommand(NavigateToDownload), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Cog", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false });
        }

        private void NavigateToDownload(object obj)
        {
            CurrentView = DownloadVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Account", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Download", NavigateCommand = new RelayCommand(NavigateToDownload), isSelected = true });
            MenuButtons.Add(new MenuButton { Name = "Cog", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = false });
        }

        private void NavigateToSettings(object obj)
        {
            CurrentView = SettingsVM;
            MenuButtons.Clear();
            MenuButtons.Add(new MenuButton { Name = "HomeVariant", NavigateCommand = new RelayCommand(NavigateToHome), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Account", NavigateCommand = new RelayCommand(NavigateToProfile), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Download", NavigateCommand = new RelayCommand(NavigateToDownload), isSelected = false });
            MenuButtons.Add(new MenuButton { Name = "Cog", NavigateCommand = new RelayCommand(NavigateToSettings), isSelected = true });
        }
    }
}
