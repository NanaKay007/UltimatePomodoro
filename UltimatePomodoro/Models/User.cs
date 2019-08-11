using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePomodoro.Models
{
    public static class User
    {
        public static string username { get; set; }
        private static string password { get; set; } 
        public static string email { get; set; }

        public static void isLoggedIn()
        {
            //gets user data and settings from the cloud

        }
    }

    
}
