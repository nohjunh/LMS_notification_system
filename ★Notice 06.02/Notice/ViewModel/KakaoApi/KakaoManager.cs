
using Newtonsoft.Json.Linq;
using Notice.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Notice.ViewModel.KakaoApi
{
    public class KakaoManager
    {
        public ViewModel VM { get; set; }
        public KakaoManager()
        { }

        public string GetUserToKen(WebBrowser webBrowser)
        {
            string wUrl = webBrowser.Source.ToString();
            string userToken = wUrl.Substring(wUrl.IndexOf("=") + 1);

            if (wUrl.CompareTo(KakaoApiEndPoint.KakaoRedirectUrl + "?code=" + userToken) == 0)
            {
                return userToken;
            }
            else
            {
                return "";
            }
        }

        public string GetAccessToKen()
        {
            var client = new RestClient(KakaoApiEndPoint.KakaoHostOAuthUrl);

            var request = new RestRequest(KakaoApiEndPoint.KakaoOAuthUrl, Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", KakaoApiEndPoint.KakaoRestApiKey);
            request.AddParameter("redirect_uri", KakaoApiEndPoint.KakaoRedirectUrl);
            request.AddParameter("code", KakaoData.userToken);

            var restResponse = client.Execute(request);
            var json = JObject.Parse(restResponse.Content);

            return json["access_token"].ToString();
        }

        public void KakaoTalkLogOut()
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

        /// <summary>
        /// 커스텀 메시지 보내기
        /// </summary>
      
        public void KakaoDefaultSendMessage(string sendMessageObject)
        {
            var client = new RestClient(KakaoApiEndPoint.KakaoHostApiUrl);

            var request = new RestRequest(KakaoApiEndPoint.KakaoDefaultMessageUrl, Method.POST);
            request.AddHeader("Authorization", "bearer " + KakaoData.accessToken);
            request.AddParameter("template_object", sendMessageObject);

            if (client.Execute(request).IsSuccessful)
            {
                MessageBox.Show("메시지 보내기 성공");
            }
            else
            {
                MessageBox.Show("메시지 보내기 실패");
            }
        }

        public void KakaoUserData()
        {
            var client = new RestClient(KakaoApiEndPoint.KakaoHostApiUrl);

            var request = new RestRequest(KakaoApiEndPoint.KakaoUserDataUrl, Method.GET);
            request.AddHeader("Authorization", "bearer " + KakaoData.accessToken);

            var restResponse = client.Execute(request);
            var json = JObject.Parse(restResponse.Content);

            KakaoData.userNickName = json["properties"]["nickname"].ToString();
        }

    }
}
