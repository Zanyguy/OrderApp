using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OrderApp.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string _UserName;
        private string _password;

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value; 
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string UserName
        {
            get { return _UserName; }
            set 
            { 
                _UserName = value;
                NotifyOfPropertyChange();
            }
        }


        public void RegisterButton()
        {

            bool userExists = SqliteDataAccess.UserExists(UserName);
            if (!userExists)
            {
                string HashedPassword = Protect(Password);
                UsersModel user = new UsersModel { UserName = UserName, Password = HashedPassword };
                SqliteDataAccess.RegisterUser(user);
            }
        }

        public void LoginButton()
        {
            UsersModel user = SqliteDataAccess.LoginAttempt(UserName);
            if (Password == Unprotect(user.Password))
            {
                
                ActivateItemAsync(new CharactersViewModel(user));
            }
        }

        public void OrdersButton()
        {
            UsersModel user = SqliteDataAccess.LoginAttempt(UserName);
            ActivateItemAsync(new OrdersViewModel(user));
        }
        public static string Protect(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
            return protectedData;
        }

        public static string Unprotect(string str)
        {
            byte[] protectedData = Convert.FromBase64String(str);
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            string data = Encoding.ASCII.GetString(ProtectedData.Unprotect(protectedData, entropy, DataProtectionScope.CurrentUser));
            return data;
        }
    }
}
