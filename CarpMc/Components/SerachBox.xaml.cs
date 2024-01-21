using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarpMc.Components
{
    public partial class SerachBox : UserControl
    {
        public SerachBox()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                string content = textBox.Text;

                //System.Diagnostics.Process.Start("https://www.google.com/search?q=" + content);
                var info = new ProcessStartInfo
                {
                    FileName = "https://www.google.com/search?q="+content,
                    UseShellExecute = true
                };
                Process.Start(info);

                e.Handled = true;
            }
        }
    }
}
