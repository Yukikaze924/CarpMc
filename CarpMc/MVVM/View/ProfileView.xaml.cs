using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CarpMc.MVVM.View
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow(this);
            new LoginWindow().Show();
            currentWindow.Close();
        }

        private void Microsoft_Window(object sender, RoutedEventArgs e)
        {
            new MicrosoftLoginView().Show();
        }

        private void Carp_Window(object sender, RoutedEventArgs e)
        {
            var info = new ProcessStartInfo
            {
                FileName = "https://formally-wanted-yak.ngrok-free.app/login",
                UseShellExecute = true
            };
            Process.Start(info);
        }
    }
}
