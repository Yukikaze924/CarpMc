﻿using Newtonsoft.Json;
using ProjBobcat.Class.Helper;
using ProjBobcat.Class.Model.Mojang;
using ProjBobcat.DefaultComponent.Launch;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.DefaultComponent.Logging;

namespace CarpMc.Utils
{
    public class Core
    {
        private static DefaultGameCore? core;

        private static Sqlite sqlite = new();

        public static DefaultGameCore InitLauncherCore()
        {
            var clientToken = new Guid("88888888-8888-8888-8888-888888888888");
            //var rootPath = Path.GetFullPath(".minecraft\");
            var rootPath = sqlite.getDataFromSettings("GamePath") ?? ".minecraft";
            core = new DefaultGameCore
            {
                ClientToken = clientToken,
                RootPath = rootPath,
                VersionLocator = new DefaultVersionLocator(rootPath, clientToken)
                {
                    LauncherProfileParser = new DefaultLauncherProfileParser(rootPath, clientToken),
                    LauncherAccountParser = new DefaultLauncherAccountParser(rootPath, clientToken)
                },
                GameLogResolver = new DefaultGameLogResolver()
            };

            return core;
        }

        public static async Task<VersionManifest?> GetVersionManifestTaskAsync()
        {
            const string vmUrl = "http://launchermeta.mojang.com/mc/game/version_manifest.json";
            var contentRes = await HttpHelper.Get(vmUrl);
            var content = await contentRes.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<VersionManifest>(content);

            return model;
        }

    }
}
