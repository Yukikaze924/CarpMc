using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpMc.MVVM.Model
{
    public class ProfileModel
    {
        public static string? Username { get; set; }
        public static string? Password { get; set; }
        public static bool IsLoggedIn { get; set; }
    }
}
