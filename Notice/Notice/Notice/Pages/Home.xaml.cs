using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notice.Classes;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Notice.Pages
{
    public partial class Home : Page
    {
        List<DepartmentData> D_Data = new List<DepartmentData>();
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;

        int countBtn2 = 0;

        public Home()
        {
            InitializeComponent();

            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            _options.AddArgument("headless");
            _options.AddArgument("disable-gpu");

            this.DataContext = new LoginViewModel();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (countBtn2 != 0)
            {
                D_Data.Clear();
            }
            Start2();
            countBtn2++;
        }     

        private async void Start2()
        {
            var task2 = Task.Run(() => SubjectCrawling());
            await task2;
            DepartmentCrawlingData.ItemsSource = D_Data;
        }
        private void SubjectCrawling()
        {
            _driver = new ChromeDriver(_driverService, _options);

            _driver.Navigate().GoToUrl("https://cse.jbnu.ac.kr/cse/3586/subview.do"); // 웹 사이트에 접속합니다.

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            string BASE_Path1 = "//*[@id='menu3586_obj176']/div[2]/form[2]/table/tbody/tr[{0}]/";
            for (int i = 1; i < 15; i++)
            {
                string url1 = string.Format(BASE_Path1, i);
                string Base_value1 = url1;
                D_Data.Add(new DepartmentData()
                {
                    D_Num = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdNum']").Text,
                    D_Title = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdTitle']").Text,
                    D_Rdate = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdRdate']").Text,

                });
            }
            _driver.Close();
        }
    }
}
