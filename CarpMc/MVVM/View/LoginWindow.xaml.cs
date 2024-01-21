using CarpMc.MVVM.Model;
using CarpMc.MVVM.ViewModel;
using CarpMc.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarpMc.MVVM.View
{
    public partial class LoginWindow : Window
    {
        private Sqlite sqlite = new Sqlite();

        private Point startPoint;

        public LoginWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
            this.MouseMove += UserControl_MouseMove;
            this.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
        }

        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username or Password can't be null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    string?[]? result = sqlite.getDataFromProfile(username, password);
                    if (username == result[1] && password == result[2])
                    {
                        ProfileModel.Username = result[1];
                        ProfileModel.Password = result[2];
                        ProfileModel.IsLoggedIn = true;
                        Thread.Sleep(200);
                        new MainWindow().Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("User does not exist!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                this.Close();
            }
        }
        private void GoRegister(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            this.Close();
        }

        private void closeButtonClicked(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(Window.GetWindow(this));
            this.CaptureMouse();
        }
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(Window.GetWindow(this));
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    parentWindow.Left += currentPoint.X - startPoint.X;
                    parentWindow.Top += currentPoint.Y - startPoint.Y;
                }
            }
        }
        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
        }
    }
}
