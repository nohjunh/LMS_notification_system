using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Windows;
using crawling.Model;

namespace crawling.ViewModel.Command
{
    public class ReportLmsCmd : ICommand
    {
        int countBtn3 = 0; // 처음 실행이 아님을 확인

        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;

        int grade = 15;

        public CrawlingVM VM { get; set; }
        public ReportLmsCmd(CrawlingVM vm)
        {
            VM = vm;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
			if (countBtn3 != 0)
			{
				VM.L_Data2.Clear();
			}
			Start3();
			countBtn3++;
		}
		private async void Start3()
		{
			var task3 = Task.Run(() => ReportCrawling());
			await task3;
			VM.get2();

		}

		public void ReportCrawling()
		{
			_driverService = ChromeDriverService.CreateDefaultService();
			_driverService.HideCommandPromptWindow = true;
			_options = new ChromeOptions();
			_options.AddArgument("headless");
			_options.AddArgument("disable-gpu");
			_driver = new ChromeDriver(_driverService, _options);


			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(VM.LoginModel.LoginID);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(VM.LoginModel.LoginPasswd);

				element = _driver.FindElementByXPath("//*[@id='loginform']/table/tbody/tr[1]/td[2]/input");
				element.Click();

				element = _driver.FindElementByXPath("//*[@id='boardAbox']/form/table/tbody/tr[1]/td[2]");
				element.Click();
			}
			catch (Exception)
			{
				MessageBox.Show("ID,PW를 확인해주세요.");
				return;
			}

			element = _driver.FindElementByXPath("//*[@id='nav']/li[4]/a");
			element.Click();

			if (grade == 21)
			{
				for (int i = 2; i < 10; i++)
				{
					element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
					element.Click();
					string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					TextUpLoad3();
				}
			}
			if (grade == 18)
			{
				for (int i = 2; i < 9; i++)
				{
					element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
					element.Click();
					string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					TextUpLoad3();
				}
			}
			if (grade == 15)
			{
				for (int i = 2; i < 8; i++)
				{
					element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
					element.Click();
					string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					TextUpLoad3();
				}
			}
			_driver.Close();
		}

		public void TextUpLoad3()
		{
			if (_driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]").Text == "해당하는 레포트 정보가 없습니다.")
			{
				VM.L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle2 = "업로드된 레포트가 없습니다."
				});
				return;
			}
			else
			{
				VM.L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsEndDate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[3]").Text,
					LmsTitle2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[2]").Text,
					LmsRdate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[6]").Text
				});
			}
		}
    }
}
