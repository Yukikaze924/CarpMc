using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CarpMc.Utils
{
    public class Page
    {
        public static void redirectToPage(Object nextPage, UserControl currentPage)
        {
            Window.GetWindow(currentPage).Content = nextPage;
        }
    }
}
