using CarpMc.Utils;
using CarpMc.MVVM.ViewModel;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using System.Windows;
using System.Windows.Controls;
using ProjBobcat.Class.Model.LauncherProfile;
using ProjBobcat.Class.Model;
using ProjBobcat.DefaultComponent.Authenticator;
using System.IO;

namespace CarpMc.Components
{
    public partial class StartButton : UserControl
    {

        private Sqlite sqlite = new Sqlite();

        private static readonly DefaultGameCore? core = Utils.Core.InitLauncherCore();

        public StartButton()
        {
            InitializeComponent();
        }

        private async void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sqlite.getDataFromSettings("JavaPath") != null && versionCombo.SelectedItem != null)
            {
                try
                {
                    string? selectedGameId = versionCombo.SelectedItem as string;
                    VersionInfo? selectedGame = core.VersionLocator.GetGame(selectedGameId);

                    var javaPath = sqlite.getDataFromSettings("JavaPath");

                    bool versionIsolation = (sqlite.getDataFromSettings("VersionIsolation") != null) ? bool.Parse(sqlite.getDataFromSettings("VersionIsolation")) : false;

                    var launchSettings = new LaunchSettings
                    {

                        Version = selectedGame.Id, // 需要启动的游戏ID
                        VersionInsulation = versionIsolation, // 版本隔离
                        GameResourcePath = core.RootPath, // 资源根目录
                        GamePath = core.RootPath, // 游戏根目录，如果有版本隔离则应该改为GamePathHelper.GetGamePath(Core.RootPath, versionId)
                        VersionLocator = core.VersionLocator, // 游戏定位器
                        GameName = selectedGame.Name,
                        GameArguments = new GameArguments // （可选）具体游戏启动参数
                        {
                            AdvanceArguments = "", // GC类型
                            JavaExecutable = javaPath, // JAVA路径
                            Resolution = new ResolutionModel { Height = 600, Width = 800 }, // 游戏窗口分辨率
                            MinMemory = 512, // 最小内存
                            MaxMemory = 1024, // 最大内存
                            GcType = GcType.G1Gc // GC类型
                        },
                        Authenticator = new OfflineAuthenticator //离线认证
                        {
                            Username = "test", //离线用户名
                            LauncherAccountParser = core.VersionLocator.LauncherAccountParser
                        }

                    };

                    var result = await core.LaunchTaskAsync(launchSettings);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("[Error]: Faild on startup!" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("[Error]: A critical error occurred!");
                e.Handled = true;
            }
        }
    }
}
