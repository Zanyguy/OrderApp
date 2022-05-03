using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OrderApp.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        

        public void LoginButton()
        {
            ActivateItemAsync(new CharactersViewModel(new DataAccess.UsersModel{ UserName = UserName, ID = 1 }));     
        }

    }
}
