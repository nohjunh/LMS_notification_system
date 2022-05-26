using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using crawling.ViewModel;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace crawling.ViewModel.Command
{
    public class LoginLmsCmd : ICommand
    {

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		public event EventHandler CanExecuteChanged;

		public CrawlingVM VM { get; set; }

		public LoginLmsCmd(CrawlingVM vm)
        {
			VM = vm;
        }

        public bool CanExecute(object parameter)
		{ 
            return true;
        }

		
        public void Execute(object parameter)
        {
			_driverService = ChromeDriverService.CreateDefaultService();
			_driverService.HideCommandPromptWindow = true;
			_options = new ChromeOptions();
			_options.AddArgument("headless");
			_options.AddArgument("disable-gpu");
			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			var parameters = (object[])parameter;
			
			IWebElement element;
				try
				{
					element = _driver.FindElementByXPath("//*[@id='id']");
				    element.SendKeys((string)parameters[0]);
				    VM.LoginModel.LoginID = ((string)parameters[0]);

					element = _driver.FindElementByXPath("//*[@id='passwd']");
				    element.SendKeys((string)parameters[1]);
				    VM.LoginModel.LoginPasswd = ((string)parameters[1]);

				    element = _driver.FindElementByXPath("//*[@id='loginform']/table/tbody/tr[1]/td[2]/input");
					element.Click();

					element = _driver.FindElementByXPath("//*[@id='nav']/li[10]/a");
					element.Click();

					MessageBox.Show("로그인 성공! 원하는 메뉴를 클릭해주세요");

					_driver.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("ID,PW를 확인해주세요.");
					return;
				}
		}
    }
}
