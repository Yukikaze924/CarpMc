using ProjBobcat.Class.Model;
using ProjBobcat.DefaultComponent;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.DefaultComponent.ResourceInfoResolver;
using ProjBobcat.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpMc.Utils
{
    public class ResourceCompleter
    {
        public DefaultGameCore core = Utils.Core.InitLauncherCore();

        public VersionInfo currentVersion;

        public ResourceCompleter(VersionInfo currentVersion)
        {
            this.currentVersion = currentVersion;
        }

        public async void completeResources()
        {
            var drc = new DefaultResourceCompleter
            {
                ResourceInfoResolvers = new List<IResourceInfoResolver>(2)
                {
                    new AssetInfoResolver
                    {
                        AssetIndexUriRoot = "https://download.mcbbs.net/",
                        AssetUriRoot = "https://download.mcbbs.net/assets/",
                        BasePath = core.RootPath,
                        VersionInfo = currentVersion
                    },
                    new LibraryInfoResolver
                    {
                        BasePath = core.RootPath,
                        LibraryUriRoot = "https://download.mcbbs.net/maven/",
                        VersionInfo = currentVersion
                    }
                }
            };

            await drc.CheckAndDownloadTaskAsync().ConfigureAwait(false);
        }
    }
}
