using Newtonsoft.Json;
using ProjBobcat.Class.Helper;
using ProjBobcat.Class.Model.Fabric;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using System.Windows;

namespace CarpMc.Utils
{
    public class FabricInstaller
    {
        private static DefaultGameCore core = Utils.Core.InitLauncherCore();

        public static async Task InstallFabricAsync()
        {
            var selectedArtifact = await Core.GetFabricArtifact();

            var fabricInstaller = new ProjBobcat.DefaultComponent.Installer.FabricInstaller
            {
                LoaderArtifact = selectedArtifact,
                VersionLocator = core.VersionLocator,
                RootPath = core.RootPath,
                CustomId = "GameId",
                InheritsFrom = "1.19.2"
            };

            await fabricInstaller.InstallTaskAsync();
        }
    }
}
