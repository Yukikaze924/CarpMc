using CarpMc.MVVM.Model;
using System.IO;
using System.Net;
using System.Windows;

namespace CarpMc
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private FileSystemWatcher watcher;

        public App()
        {
            ServicePointManager.DefaultConnectionLimit = 512;

            watcher = new FileSystemWatcher();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // 在应用程序启动时进行静态类字段的访问
            watcher.Path = Utils.Core.InitLauncherCore().RootPath;
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security;

            watcher.Created += (o, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Versions.VersionList.Clear();
                    foreach (var verion in Utils.Core.InitLauncherCore().VersionLocator.GetAllGames())
                    {
                        Versions.VersionList.Add(verion.Id);
                    };
                });
            };
            watcher.Changed += (o, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Versions.VersionList.Clear();
                    foreach (var verion in Utils.Core.InitLauncherCore().VersionLocator.GetAllGames())
                    {
                        Versions.VersionList.Add(verion.Id);
                    };
                });
            };
            watcher.Deleted += (o, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Versions.VersionList.Clear();
                    foreach (var verion in Utils.Core.InitLauncherCore().VersionLocator.GetAllGames())
                    {
                        Versions.VersionList.Add(verion.Id);
                    };
                });
            };
            watcher.EnableRaisingEvents = true;
        }
    }
}
