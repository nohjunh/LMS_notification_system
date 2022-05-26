using Notice.Classes;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notice.ViewModel.Command.HomePageCommand
{
    public class DepartmentDataButtonCommand : ICommand
    {
        int countBtn2 = 0;

        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;

        
        
        public event EventHandler CanExecuteChanged;
        public ViewModel VM { get; set; }

        public DepartmentDataButtonCommand(ViewModel vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (countBtn2 != 0)
            {
                VM.D_Data.Clear();
            }
            Start2();
            countBtn2++;
        }

        private async void Start2()
        {
            var task2 = Task.Run(() => SubjectCrawling());
            await task2;
            View.Home home = new View.Home();
            home.DepartmentCrawlingData.ItemsSource = VM.D_Data;
        }
        private void SubjectCrawling()
        {
            _driverService = ChromeDriverService.CreateDefaultService();
           // _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            //_options.AddArgument("headless");
            _options.AddArgument("disable-gpu");
            _driver = new ChromeDriver(_driverService, _options);

            _driver.Navigate().GoToUrl("https://cse.jbnu.ac.kr/cse/3586/subview.do"); // 웹 사이트에 접속합니다.

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            string BASE_Path1 = "//*[@id='menu3586_obj176']/div[2]/form[2]/table/tbody/tr[{0}]/";
            for (int i = 1; i < 15; i++)
            {
                string url1 = string.Format(BASE_Path1, i);
                string Base_value1 = url1;
                VM.D_Data.Add(new DepartmentData()
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
