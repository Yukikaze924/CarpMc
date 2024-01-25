using CarpMc.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Panuon.UI.Silver;
using ProjBobcat.Class.Helper;
using ProjBobcat.DefaultComponent.Launch;
using ProjBobcat.Platforms.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace CarpMc.MVVM.ViewModel
{
     partial class SettingsViewModel : ObservableObject
     {
        private Sqlite sqlite = new Sqlite();

        [ObservableProperty]
        private string? _javaPath;

        [ObservableProperty]
        //private List<string>? _javaList;
        private ObservableCollection<string>? _javaList;

        [ObservableProperty]
        private ObservableCollection<string>? _gameList;

        [ObservableProperty]
        private string? _gamePath;

        [ObservableProperty]
        private bool versionIsolation;

        public SettingsViewModel()
        {
            sqlite.initializeDatabaseTable();

            JavaPath = sqlite.getDataFromSettings("JavaPath");            

            GamePath = sqlite.getDataFromSettings("GamePath") ?? ".minecraft";

            JavaList = new ObservableCollection<string>(ProjBobcat.Class.Helper.SystemInfoHelper.FindJava().ToListAsync().Result);
            if (JavaPath!=null)
            {
                JavaList.Add(JavaPath);
            }

            GameList = new ObservableCollection<string>(Utils.Core.InitLauncherCore().VersionLocator.GetAllGames().ToList().Select(v => v.Id).ToList());

            VersionIsolation = sqlite.getDataFromSettings("VersionIsolation")!=null ? bool.Parse(sqlite.getDataFromSettings("VersionIsolation")) : false;
        }

        [RelayCommand]
        private void VersionIsolationToggleButtonClicked()
        {
            string isChecked = VersionIsolation ? "true" : "false";
            sqlite.insertDataIntoSettings("VersionIsolation", isChecked);
        }

        [RelayCommand]
        private void AddJavaIntoList()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Java Runtime";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;                  

                    sqlite.insertDataIntoSettings("JavaPath", selectedFilePath);

                    JavaList.Add(selectedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        [RelayCommand]
        private void JavaListPropertyChanged()
        {
            string? selectedJavaPath = JavaPath;
            sqlite.insertDataIntoSettings("JavaPath", selectedJavaPath);
        }

        [RelayCommand]
        private void GameListPropertyChanged()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select a Minecraft Directory";

            if (openFolderDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFolderPath = openFolderDialog.FolderName;

                    sqlite.insertDataIntoSettings("GamePath", selectedFolderPath);

                    //var gameStringList = new DefaultVersionLocator(selectedFolderPath, Utils.Core.InitLauncherCore().ClientToken)
                    //{
                    //    LauncherProfileParser = new DefaultLauncherProfileParser(selectedFolderPath, Utils.Core.InitLauncherCore().ClientToken),
                    //    LauncherAccountParser = new DefaultLauncherAccountParser(selectedFolderPath, Utils.Core.InitLauncherCore().ClientToken)
                    //}.GetAllGames().ToList().Select(v => v.Id).ToList();
                    //GameList.Clear();
                    //GameList = new ObservableCollection<string>(gameStringList);

                    MessageBoxResult result = MessageBox.Show("Do you want to restart? (Recommended)", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.OK:

                            System.Reflection.Assembly.GetEntryAssembly();
                            string startpath = System.IO.Directory.GetCurrentDirectory();
                            System.Diagnostics.Process.Start(startpath + "\\CarpMc.exe");
                            Application.Current.Shutdown();

                            break;

                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
