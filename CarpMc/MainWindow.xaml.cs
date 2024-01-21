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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarpMc.MVVM.ViewModel;
using CarpMc.Utils;

namespace CarpMc
{
    public partial class MainWindow : Window
    {
        private Sqlite sqlite = new Sqlite();

        private Point startPoint;

        private bool isClosingStoryBoardCompleted = false;
        private bool isWindowMinimize = false;

        public MainWindow()
        {
            InitializeComponent();
            sqlite.initializeDatabaseTable();

            this.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
            this.MouseMove += UserControl_MouseMove;
            this.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
        }

        private void closeButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                string content = textBox.Text;

                System.Diagnostics.Process.Start("https://www.google.com/search?q="+content);

                e.Handled = true;
            }
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

        private void minimizeButtonClicked(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosingStoryBoardCompleted)
            {
                Storyboard storyboard = (Storyboard)this.Resources["FadeOutStoryboard"];
                storyboard.Begin(this);
                e.Cancel = true;
            }
        }
        private void closeStoryBoard_Completed(object sender, EventArgs e)
        {
            isClosingStoryBoardCompleted = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard loadingAnimation = (Storyboard)this.Resources["WindowLoadingAnimation"];
            loadingAnimation.Begin();
        }
    }
}
