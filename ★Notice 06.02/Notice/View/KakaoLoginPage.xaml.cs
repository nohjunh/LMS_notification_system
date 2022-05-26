using Notice.Model;
using Notice.ViewModel.KakaoApi;
using Notice.ViewModel;
using System.Windows;
using System.Windows.Navigation;

namespace Notice.View
{
    /// <summary>
    /// KakaoLoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KakaoLoginPage : Window
    {
        KakaoManager kakaoManager;
        
        
        public KakaoLoginPage()
        {
            InitializeComponent();

            kakaoManager = new KakaoManager();

            WebBrowser1.LoadCompleted += WebBrowser1_DocumentCompleted;

            WebBrowser1.Navigate(KakaoApiEndPoint.KakaoLogInUrl);
        }

        private void WebBrowser1_DocumentCompleted(object sender, NavigationEventArgs e)
        {
            string code = kakaoManager.GetUserToKen(WebBrowser1);
            if (code != "")
            {
                
                KakaoData.userToken = code;
                KakaoData.accessToken = kakaoManager.GetAccessToKen();
                this.Close();
            }
        }
    }
}
