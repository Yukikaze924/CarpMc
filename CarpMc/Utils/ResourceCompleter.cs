using Newtonsoft.Json;
using ProjBobcat.Class.Model;
using ProjBobcat.Class.Model.Mojang;
using ProjBobcat.DefaultComponent;
using ProjBobcat.DefaultComponent.Launch.GameCore;
using ProjBobcat.DefaultComponent.ResourceInfoResolver;
using ProjBobcat.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarpMc.Utils
{
    public class ResourceCompleter
    {
        public static DefaultGameCore core = Utils.Core.InitLauncherCore();

        public static async Task DownloadResourcesAsync(VersionInfo versionInfo)
        {
            var versions = await Utils.Core.GetVersionManifestTaskAsync();
            var rc = new DefaultResourceCompleter
            {
                CheckFile = true,
                DownloadParts = 8,
                ResourceInfoResolvers = new List<IResourceInfoResolver>
                {
                    new VersionInfoResolver
                    {
                        BasePath = core.RootPath,
                        VersionInfo = versionInfo,
                        CheckLocalFiles = true
                    },
                    new AssetInfoResolver
                    {
                        BasePath = core.RootPath,
                        VersionInfo = versionInfo,
                        CheckLocalFiles = true,
                        Versions = versions?.Versions
                    },
                    new LibraryInfoResolver
                    {
                        BasePath = core.RootPath,
                        VersionInfo = versionInfo,
                        CheckLocalFiles = true,
                    }
                },
                MaxDegreeOfParallelism = 8,
                TotalRetry = 2
            };

            await rc.CheckAndDownloadTaskAsync();
        }

        //public async void initInfoResolver()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync("https://launchermeta.mojang.com/mc/game/version_manifest.json");
        //            response.EnsureSuccessStatusCode(); // 确保请求成功

        //            var responseJson = await response.Content.ReadAsStringAsync();

        //            // 将 JSON 响应转换为 ProjBobcat 类型
        //            var manifest = JsonConvert.DeserializeObject<VersionManifest>(responseJson);

        //            // 获取 Versions 列表
        //            var versions = manifest.Versions;

        //            assetInfoResolver = new AssetInfoResolver
        //            {
        //                AssetIndexUriRoot = "https://launchermeta.mojang.com/",
        //                AssetUriRoot = "https://resources.download.minecraft.net/",
        //                BasePath = core.RootPath,
        //                VersionInfo = currentVersion,
        //                CheckLocalFiles = true,
        //                Versions = versions // 在上一步获取到的 Versions 数组
        //            };

        //            libraryInfoResolver = new LibraryInfoResolver
        //            {
        //                BasePath = core.RootPath,
        //                ForgeUriRoot = "https://files.minecraftforge.net/maven/",
        //                ForgeMavenUriRoot = "https://maven.minecraftforge.net/",
        //                ForgeMavenOldUriRoot = "https://files.minecraftforge.net/maven/",
        //                FabricMavenUriRoot = "https://maven.fabricmc.net/",
        //                LibraryUriRoot = "https://libraries.minecraft.net/",
        //                VersionInfo = currentVersion,
        //                CheckLocalFiles = true
        //            };

        //            versionInfoResolver = new VersionInfoResolver
        //            {
        //                BasePath = core.RootPath,
        //                VersionInfo = currentVersion,
        //                CheckLocalFiles = true
        //            };
        //        }
        //        catch (HttpRequestException e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //    }
        //}

        //public async void completeResources()
        //{
        //    var completer = new DefaultResourceCompleter
        //    {
        //        MaxDegreeOfParallelism = 2,
        //        ResourceInfoResolvers = new List<IResourceInfoResolver>
        //        {
        //            assetInfoResolver, libraryInfoResolver, versionInfoResolver
        //        },
        //        TotalRetry = 2,
        //        CheckFile = true,
        //        DownloadParts = 2
        //    };

        //    var result = await completer.CheckAndDownloadTaskAsync();

        //    if (result.TaskStatus == TaskResultStatus.Error && (result.Value?.IsLibDownloadFailed ?? false))
        //    {
        //        // 在完成补全后，资源检查器会返回执行结果。
        //        // 您可以检查 result 中的属性值来确定补全是否完成
        //        MessageBox.Show("Error");
        //        // IsLibDownloadFailed 会反映启动必须的库文件是否已经成功补全
        //        // 通常来说，如果库文件的补全失败，很有可能会导致游戏的启动失败
        //    }
        //}
    }
}
