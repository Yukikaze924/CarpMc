using ProjBobcat.DefaultComponent.Installer.ForgeInstaller;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarpMc.Utils
{
    public class ForgeInstaller
    {
        private static DefaultGameCore core = Utils.Core.InitLauncherCore();

        private static Sqlite sqlite = new();

        public static async Task InstallForgeAsync()
        {
            var mcVersion = "1.16.5";
            var forgeJarPath = "C:\\Users\\27378\\.minecraft\\versions\\forge-1.16.5-36.2.34-installer.jar";
            var forgeVersion = ForgeInstallerFactory.GetForgeArtifactVersion(mcVersion, "36.2.34");

            var isLegacy = ForgeInstallerFactory.IsLegacyForgeInstaller(forgeJarPath, forgeVersion);

            IForgeInstaller forgeInstaller = isLegacy
            ? new LegacyForgeInstaller
            {
                ForgeExecutablePath = forgeJarPath,
                RootPath = core.RootPath,
                CustomId = "1.16.5-forge",
                ForgeVersion = forgeVersion,
                InheritsFrom = "1.16.5"
            }
            : new HighVersionForgeInstaller
            {
                ForgeExecutablePath = forgeJarPath,
                JavaExecutablePath = sqlite.getDataFromSettings("JavaPath"),
                RootPath = core.RootPath,
                VersionLocator = core.VersionLocator,
                DownloadUrlRoot = "https://bmclapi2.bangbang93.com/",
                CustomId = "1.16.5-forge-36.2.34",
                MineCraftVersion = "1.16.5",
                MineCraftVersionId = "1.16.5",
                InheritsFrom = "1.16.5"
            };

            await forgeInstaller.InstallForgeTaskAsync();
        }
    }
}
