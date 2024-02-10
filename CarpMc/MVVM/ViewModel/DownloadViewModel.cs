using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using ProjBobcat.Class.Helper;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace CarpMc.MVVM.ViewModel
{
    partial class DownloadViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<string> _mcVersions;

        [ObservableProperty]
        private string? _mcVersion;

        [ObservableProperty]
        private string? _forgeMcVersion;

        [ObservableProperty]
        private ObservableCollection<string> _forgeVersions;

        public DownloadViewModel()
        {
            McVersions = new List<string>()
        {
            "1.7.10",
            "1.8.0",
            "1.8.1",
            "1.8.2",
            "1.8.3",
            "1.8.4",
            "1.8.5",
            "1.8.6",
            "1.8.7",
            "1.8.8",
            "1.8.9",
            "1.9.0",
            "1.9.1",
            "1.9.2",
            "1.9.3",
            "1.9.4",
            "1.10.0",
            "1.10.1",
            "1.10.2",
            "1.11.0",
            "1.11.1",
            "1.11.2",
            "1.12.0",
            "1.12.1",
            "1.12.2",
            "1.13.0",
            "1.13.1",
            "1.13.2",
            "1.14.0",
            "1.14.1",
            "1.14.2",
            "1.14.3",
            "1.14.4",
            "1.15.0",
            "1.15.1",
            "1.15.2",
            "1.16.0",
            "1.16.1",
            "1.16.2",
            "1.16.3",
            "1.16.4",
            "1.16.5",
            "1.17.0",
            "1.17.1",
            "1.17.2",
            "1.18.0",
            "1.18.1",
            "1.18.2"
        };
            ForgeVersions = [];
        }

        [RelayCommand]
        private async Task DownloadVanillaMinecraftAsync()
        {
            if (Utils.Core.InitLauncherCore().VersionLocator.GetGame(McVersion) == null)
            {
                try
                {
                    string apiUrl = "https://bmclapi2.bangbang93.com/version/" + McVersion;
                    string path = Utils.Core.InitLauncherCore().RootPath + "\\versions\\" + McVersion;

                    Directory.CreateDirectory(Path.GetDirectoryName(path + "\\" + McVersion + ".jar"));
                    var jarResponse = await HttpHelper.Get(apiUrl + "/client");
                    byte[] jarContent = await jarResponse.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(path + "\\" + McVersion + ".jar", jarContent);

                    Directory.CreateDirectory(Path.GetDirectoryName(path + "\\" + McVersion + ".json"));
                    var jsonResponse = await HttpHelper.Get(apiUrl + "/json");
                    string jsonContent = await jsonResponse.Content.ReadAsStringAsync();
                    File.WriteAllText(path + "\\" + McVersion + ".json", jsonContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("[Error]: Version " + ForgeMcVersion + " already exist!"); return;
            }
        }

        [RelayCommand]
        private async Task DownloadForgeInstallerAsync()
        {
            if (Utils.Core.InitLauncherCore().VersionLocator.GetGame(ForgeMcVersion) == null)
            {
                string apiUrl = "https://bmclapi2.bangbang93.com/version/" + ForgeMcVersion;
                string path = Utils.Core.InitLauncherCore().RootPath + "\\versions\\" + ForgeMcVersion;

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path + "\\" + ForgeMcVersion + ".jar"));
                    var jarResponse = await HttpHelper.Get(apiUrl + "/client");
                    byte[] jarContent = await jarResponse.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(path + "\\" + ForgeMcVersion + ".jar", jarContent);

                    Directory.CreateDirectory(Path.GetDirectoryName(path + "\\" + ForgeMcVersion + ".json"));
                    var jsonResponse = await HttpHelper.Get(apiUrl + "/json");
                    string jsonContent = await jsonResponse.Content.ReadAsStringAsync();
                    File.WriteAllText(path + "\\" + ForgeMcVersion + ".json", jsonContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                MessageBox.Show("Done!");
            }
            else
            {
                MessageBox.Show("[Error]: Version " + ForgeMcVersion + " already exist!"); return;
            }
        }

        [RelayCommand]
        private async Task McVersionPropertyChanged()
        {
            ForgeVersions.Clear();

            var response = await HttpHelper.Get("https://bmclapi2.bangbang93.com/forge/minecraft/"+ForgeMcVersion);

            string content = await response.Content.ReadAsStringAsync();

            var json = JArray.Parse(content);

            for (int i = 0; i < json.Count; i++)
            {
                ForgeVersions.Add(json[i]["version"].ToString());
            }
        }
    }
}
