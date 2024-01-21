using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarpMc.MVVM.Model
{
    public class MenuButton
    {
        public string Name { get; set; }
        public ICommand NavigateCommand { get; set; }
        public bool isSelected { get; set; }
    }
}
