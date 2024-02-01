using CarpMc.MVVM.Model;
using CarpMc.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Panuon.UI.Silver;
using ProjBobcat.Class.Helper;
using ProjBobcat.DefaultComponent.Launch;
using ProjBobcat.Platforms.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace CarpMc.MVVM.ViewModel
{
     partial class SettingsViewModel : ObservableObject
     {
        private Sqlite sqlite = new Sqlite();

        private HashSet<string> hashSet = new HashSet<string>();

        [ObservableProperty]
        private string? _javaPath;

        [ObservableProperty]
        private ObservableCollection<string>? _javaList;

        [ObservableProperty]
        private string? _gamePath;

        [ObservableProperty]
        private bool versionIsolation;

        public SettingsViewModel()
        {
            sqlite.initializeDatabaseTable();
            hashSet.UnionWith(ProjBobcat.Class.Helper.SystemInfoHelper.FindJava().ToListAsync().Result);

            JavaPath = sqlite.getDataFromSettings("JavaPath");            

            GamePath = sqlite.getDataFromSettings("GamePath") ?? ".minecraft";

            JavaList = new ObservableCollection<string>(ProjBobcat.Class.Helper.SystemInfoHelper.FindJava().ToListAsync().Result);
            if (JavaPath!=null && hashSet.Contains(JavaPath)==false)
            {
                JavaList.Add(JavaPath); hashSet.Add(JavaPath);
            }

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

                    if (hashSet.Contains(selectedFilePath))
                    {
                        return;
                    }

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

                    sqlite.insertDataIntoSettings("GamePath", selectedFolderPath); GamePath = selectedFolderPath;

                    MessageBoxResult result = MessageBox.Show("Do you want to restart? (Recommended)", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    switch (result)
                    {
                        case MessageBoxResult.OK:

                            System.Reflection.Assembly.GetEntryAssembly();
                            string startpath = System.IO.Directory.GetCurrentDirectory();
                            System.Diagnostics.Process.Start(startpath + "\\CarpMc.exe");
                            Application.Current.Shutdown();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //Versions.VersionList.Clear();
                //foreach (var verion in Utils.Core.InitLauncherCore().VersionLocator.GetAllGames())
                //{
                //    Versions.VersionList.Add(verion.Id);
                //};
            }
        }
    }
}
