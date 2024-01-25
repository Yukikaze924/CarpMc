using CarpMc.Components;
using CarpMc.MVVM.ViewModel;
using CarpMc.Utils;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CarpMc.MVVM.View
{
    public partial class SettingsView : UserControl
    {
        private Sqlite sqlite = new Sqlite();

        public SettingsView()
        {
            InitializeComponent();

            this.DataContext = new SettingsViewModel();
        }

        private void JavaSelectorClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Java Runtime";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;

                    selectedFolderTextBox.Text = selectedFilePath;

                    sqlite.insertDataIntoSettings("JavaPath", selectedFilePath);
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void gamePathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select a Minecraft Directory";

            if (openFolderDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFolderPath = openFolderDialog.FolderName;

                    gamePathTextBox.Text = selectedFolderPath;

                    sqlite.insertDataIntoSettings("GamePath", selectedFolderPath);

                    MessageBoxResult result = MessageBox.Show("Do you want to restart? (Recommended)", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    switch(result)
                    {
                        case MessageBoxResult.OK:

                            //Window currentWindow = Window.GetWindow(this);

                            //currentWindow.Hide();
                            //Thread.Sleep(1000);
                            //new MainWindow().Show();
                            //currentWindow.Close();

                            System.Reflection.Assembly.GetEntryAssembly();
                            string startpath = System.IO.Directory.GetCurrentDirectory();
                            System.Diagnostics.Process.Start(startpath + "\\CarpMc.exe");
                            Application.Current.Shutdown();

                            break;

                        case MessageBoxResult.Cancel:
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
