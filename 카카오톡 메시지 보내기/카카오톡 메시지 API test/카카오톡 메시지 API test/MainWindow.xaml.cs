using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 카카오톡_메시지_API_test.Scripts;

namespace 카카오톡_메시지_API_test
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private KakaoLoginPage kakaoLoginPage;
        private KakaoManager kakaoManager;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += KakaoMain_Load; // 로드 이벤트 추가
            kakaoManager = new KakaoManager();
            Console.WriteLine(KakaoApiEndPoint.KakaoLogInUrl);
            

        }

        private void KakaoMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("로드");

            WebBrowserVersionSetting();
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("로그인 버튼");
            kakaoLoginPage = new KakaoLoginPage();
            kakaoLoginPage.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("로그아웃 버튼");
            kakaoManager.KakaoTalkLogOut();
        }

        private void templateMessage_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("템플릿 메시지 보내기 버튼");
            kakaoManager.KakaoTemplateSendMessage(KakaoApiEndPoint.KakaoSendMessageKey);
        }

        private void customMessage_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("커스텀 메시지 보내기 버튼");
            JObject SendJson = new JObject();
            JObject LinkJson = new JObject();

            LinkJson.Add("web_url", "https://developers.kakao.com");
            LinkJson.Add("mobile_web_url", "https://developers.kakao.com");

            SendJson.Add("object_type", "text");
            SendJson.Add("text", "커스텀 메시지 입니다. https://github.com/Byuntil \n\n");
            SendJson.Add("link", LinkJson);
            SendJson.Add("button_title", "안녕");

            Console.WriteLine(SendJson);

            kakaoManager.KakaoDefaultSendMessage(SendJson);
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
        private void userData_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("유저 데이터");
            kakaoManager.KakaoUserData();

            PictureBox_UserImg.Source = ImageSourceFromBitmap(KakaoData.UserImg);
            Label_UserName.Content = KakaoData.UserNickName;
        }

        public enum BrowserEmulationVersion
        {
            Default = 0,
            Version7 = 7000,
            Version8 = 8000,
            Version8Standards = 8888,
            Version9 = 9000,
            Version9Standards = 9999,
            Version10 = 10000,
            Version10Standards = 10001,
            Version11 = 11000,
            Version11Edge = 11001
        }

        public class InternetExplorerBrowserEmulation
        {
            private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";
            private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";


            public static int GetInternetExplorerMajorVersion()
            {
                int result;

                result = 0;


                RegistryKey key;

                key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                if (key != null)
                {
                    object value;

                    value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                    if (value != null)
                    {
                        string version;
                        int separator;

                        version = value.ToString();
                        separator = version.IndexOf('.');
                        if (separator != -1)
                        {
                            int.TryParse(version.Substring(0, separator), out result);
                        }
                    }
                }
                


                return result;
            }

            public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
            {
                bool result;

                result = false;


                RegistryKey key;

                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                if (key != null)
                {
                    string programName;

                    programName = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                    if (browserEmulationVersion != BrowserEmulationVersion.Default)
                    {
                        // if it's a valid value, update or create the value
                        key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                    }
                    else
                    {
                        // otherwise, remove the existing value
                        key.DeleteValue(programName, false);
                    }

                    result = true;
                }
                

                return result;
            }

            public static bool SetBrowserEmulationVersion()
            {
                int ieVersion;
                BrowserEmulationVersion emulationCode;

                ieVersion = GetInternetExplorerMajorVersion();

                if (ieVersion >= 11)
                {
                    emulationCode = BrowserEmulationVersion.Version11;
                }
                else
                {
                    switch (ieVersion)
                    {
                        case 10:
                            emulationCode = BrowserEmulationVersion.Version10;
                            break;
                        case 9:
                            emulationCode = BrowserEmulationVersion.Version9;
                            break;
                        case 8:
                            emulationCode = BrowserEmulationVersion.Version8;
                            break;
                        default:
                            emulationCode = BrowserEmulationVersion.Version7;
                            break;
                    }
                }

                return SetBrowserEmulationVersion(emulationCode);
            }

            public static BrowserEmulationVersion GetBrowserEmulationVersion()
            {
                BrowserEmulationVersion result;

                result = BrowserEmulationVersion.Default;

                RegistryKey key;

                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                if (key != null)
                {
                    string programName;
                    object value;

                    programName = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    value = key.GetValue(programName, null);

                    if (value != null)
                    {
                        result = (BrowserEmulationVersion)Convert.ToInt32(value);
                    }
                }
                


                return result;
            }


            public static bool IsBrowserEmulationSet()
            {
                return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
            }
        }

        private void WebBrowserVersionSetting()
        {
            RegistryKey registryKey = null; // 레지스트리 변경에 사용 될 변수

            int browserver = 0;
            int ie_emulation = 0;
            var targetApplication = Process.GetCurrentProcess().ProcessName + ".exe"; // 현재 프로그램 이름
            try
            {
                registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                if(!InternetExplorerBrowserEmulation.IsBrowserEmulationSet())
                {
                    InternetExplorerBrowserEmulation.SetBrowserEmulationVersion();
                    browserver = InternetExplorerBrowserEmulation.GetInternetExplorerMajorVersion();
                    if (browserver >= 11)
                        ie_emulation = 11001;
                    else if (browserver == 10)
                        ie_emulation = 10001;
                    else if (browserver == 9)
                        ie_emulation = 9999;
                    else if (browserver == 8)
                        ie_emulation = 8888;
                    else
                        ie_emulation = 7000;
                }
                // IE가 없으면 실행 불가능
                if (registryKey == null)
                {
                    MessageBox.Show("웹 브라우저 버전 초기화에 실패했습니다..!");
                    System.Windows.Application.Current.Shutdown();
                    return;
                }

                string FindAppkey = Convert.ToString(registryKey.GetValue(targetApplication));

                // 이미 키가 있다면 종료
                if (FindAppkey == ie_emulation.ToString())
                {
                    registryKey.Close();
                    return;
                }

                // 키가 없으므로 키 셋팅
                registryKey.SetValue(targetApplication, unchecked((int)ie_emulation), RegistryValueKind.DWord);

                // 다시 키를 받아와서
                FindAppkey = Convert.ToString(registryKey.GetValue(targetApplication));

                // 현재 브라우저 버전이랑 동일 한지 판단
                if (FindAppkey == ie_emulation.ToString())
                {
                    return;
                }
                else
                {
                    MessageBox.Show("웹 브라우저 버전 초기화에 실패했습니다..!");
                    System.Windows.Application.Current.Shutdown();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("웹 브라우저 버전 초기화에 실패했습니다..!");
                System.Windows.Application.Current.Shutdown();
                return;
            }
            finally
            {
                // 키 메모리 해제
                if (registryKey != null)
                {
                    registryKey.Close();
                }
            }
        }
    }
}
