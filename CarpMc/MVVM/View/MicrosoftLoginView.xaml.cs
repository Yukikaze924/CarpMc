using microsoft_launcher;
using System.Windows;

namespace CarpMc.MVVM.View
{
    public partial class MicrosoftLoginView : Window
    {
        public MicrosoftLoginView()
        {
            InitializeComponent();

            MicrosoftAPIs microsoftAPIs = new MicrosoftAPIs();

            microsoftAPIs.SuppressWininetBehavior();

            WebBrowser.Source = microsoftAPIs.loginWebsite;
        }
    }
}
