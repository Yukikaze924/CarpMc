using CarpMc.Utils;
using ProjBobcat.Platforms.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace CarpMc.MVVM.ViewModel
{
     class SettingsViewModel : Core.ViewModel
    {
        private Sqlite sqlite = new Sqlite();

        private string? _javaPath;
        public string? JavaPath { 

            get { 
                return _javaPath;
            }
            set
            {
                _javaPath = value;
                OnPropertyChanged(nameof(JavaPath));
            }
        }

        private string? _javaList;
        public string? JavaList
        {
            get { return _javaList; }
            set
            {
                _javaList = value;
                OnPropertyChanged(nameof(JavaList));
            }
        }

        private string? _gamePath;
        public string? GamePath
        {
            get
            {
                return _gamePath;
            }
            set
            {
                _gamePath = value;
                OnPropertyChanged(nameof(GamePath));
            }
        }

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
        }

    }
}
