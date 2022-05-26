using Microsoft.Win32;
using Notice.View;
using Notice.ViewModel.KakaoApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notice.ViewModel.Command.Binding
{
    public class KakaoLogin : ICommand
    {
        private KakaoLoginPage kakaoLoginPage;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //version setting
            kakaoLoginPage = new KakaoLoginPage();
            kakaoLoginPage.ShowDialog();
        }
    }
}
