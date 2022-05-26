using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.ViewModel.KakaoApi
{
    public class KakaoApiEndPoint
    {
        // API 키
        public const string KakaoRestApiKey = "c870399b6691489a0c199e83ddd6c0a7";
        public const string KakaoSendMessageKey = "53389";

        // 리다이렉트 url
        public const string KakaoRedirectUrl = "https://www.naver.com/oauth";

        // 로그인 url
        public const string KakaoLogInUrl = "https://kauth.kakao.com/oauth/authorize?client_id=" + KakaoRestApiKey + "&redirect_uri=" + KakaoRedirectUrl + "&response_type=code";

        // 루트 url
        public const string KakaoHostOAuthUrl = "https://kauth.kakao.com";
        public const string KakaoHostApiUrl = "https://kapi.kakao.com";

        // 이벤트 url
        public const string KakaoOAuthUrl = "/oauth/token"; // AccessToken
        public const string KakaoUnlinkUrl = "/v1/user/unlink"; // LogOut
        public const string KakaoDefaultMessageUrl = "/v2/api/talk/memo/default/send"; // Default 메시지
        public const string KakaoUserDataUrl = "/v2/user/me"; // 사용자 데이터
    }
}
