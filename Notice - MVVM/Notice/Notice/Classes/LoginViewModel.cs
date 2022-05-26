using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Classes
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _loginId;
        public string LoginID
        {
            get
            {
                return _loginId;
            }
            set
            {
                _loginId = value;
                OnPropertyUpdate("LoginID");
            }
        }
        private string _loginPasswd;
        public string LoginPasswd
        {
            get
            {
                return _loginPasswd;
            }
            set
            {
                _loginPasswd = value;
                OnPropertyUpdate("LoginPasswd");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
