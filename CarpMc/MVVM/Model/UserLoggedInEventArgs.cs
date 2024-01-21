using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpMc.MVVM.Model
{
    public class UserLoggedInEventArgs : EventArgs
    {
        private string[] row;

        public UserLoggedInEventArgs(string[] row)
        {
            this.row = row;
        }
    }
}
