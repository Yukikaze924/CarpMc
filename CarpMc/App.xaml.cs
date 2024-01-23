using System.Net;
using System.Windows;

namespace CarpMc
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServicePointManager.DefaultConnectionLimit = 512;
        }
    }
}
