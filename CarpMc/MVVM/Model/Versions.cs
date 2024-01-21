using ProjBobcat.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpMc.MVVM.Model
{
    public class Versions
    {
        public static List<VersionInfo>? VersionList { get; set; }

        public Versions()
        {
            VersionList = Utils.Core.InitLauncherCore().VersionLocator.GetAllGames().ToList();
        }
    }
}
