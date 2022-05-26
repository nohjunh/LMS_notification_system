using Notice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notice.ViewModel.Command.Binding
{
    public class NaverBinding : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string email = parameter.ToString();
            if (email.Contains("@naver.com"))
            {
                NaverData.userEmail = parameter.ToString();
                MessageBox.Show("성공했습니다.");
			}
            else
            {
				MessageBox.Show("이메일을 다시 입력해주세요");
            }
        }
    }
}
