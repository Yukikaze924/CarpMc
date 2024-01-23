using CarpMc.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjBobcat.Platforms.Windows;
using System.Windows;

namespace CarpMc.MVVM.ViewModel
{
     partial class SettingsViewModel : ObservableObject
     {
        private Sqlite sqlite = new Sqlite();

        [ObservableProperty]
        private string? _javaPath;
        //public string? JavaPath { 

        //    get { 
        //        return _javaPath;
        //    }
        //    set
        //    {
        //        _javaPath = value;
        //        OnPropertyChanged(nameof(JavaPath));
        //    }
        //}

        [ObservableProperty]
        private string? _gamePath;
        //public string? GamePath
        //{
        //    get
        //    {
        //        return _gamePath;
        //    }
        //    set
        //    {
        //        _gamePath = value;
        //        OnPropertyChanged(nameof(GamePath));
        //    }
        //}

        [ObservableProperty]
        private bool versionIsolation;

        public SettingsViewModel()
        {
            sqlite.initializeDatabaseTable();

            try
            {
                JavaPath = sqlite.getDataFromSettings("JavaPath") ?? SystemInfoHelper.FindJavaWindows().ToList().First();
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "Java is not set up correctly");
            }
            try
            {
                GamePath = sqlite.getDataFromSettings("GamePath") ?? ".minecraft";
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "The game directory is not set correctly");
            }

            VersionIsolation = bool.Parse(sqlite.getDataFromSettings("VersionIsolation"));
        }

        [RelayCommand]
        private void VersionIsolationToggleButtonClicked()
        {
            string isChecked = VersionIsolation ? "true" : "false";
            sqlite.insertDataIntoSettings("VersionIsolation", isChecked);
        }
    }
}
