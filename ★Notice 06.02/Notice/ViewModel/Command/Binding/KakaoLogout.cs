using Notice.Model;
using Notice.ViewModel.KakaoApi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notice.ViewModel.Command.Binding
{
    public class KakaoLogout : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var client = new RestClient(KakaoApiEndPoint.KakaoHostApiUrl);

            var request = new RestRequest(KakaoApiEndPoint.KakaoUnlinkUrl, Method.POST);
            request.AddHeader("Authorization", "bearer " + KakaoData.accessToken);

            if (client.Execute(request).IsSuccessful)
            {
                MessageBox.Show("로그아웃 성공");
            }
            else
            {
                MessageBox.Show("로그아웃 실패");
            }
        }
    }
}
