using Notice.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Windows;
using System.Diagnostics;

namespace Notice.ViewModel.Command.LoginPageCommand
{
    public class signinButtonCommand : ICommand
    {
		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		public event EventHandler CanExecuteChanged;

		public ViewModel VM { get; set; }

		public signinButtonCommand(ViewModel vm)
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
				VM.mainWindowIDInformation.LoginID = ((string)parameters[0]);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys((string)parameters[1]);
				VM.mainWindowIDInformation.LoginPasswd = ((string)parameters[1]);

				element = _driver.FindElementByXPath("//*[@id='loginform']/table/tbody/tr[1]/td[2]/input");
				element.Click();

				element = _driver.FindElementByXPath("//*[@id='nav']/li[10]/a");
				element.Click();

				MessageBox.Show("로그인 성공 !\n원하시는 메뉴를 선택해주세요.");
				/*
				First first = new First();
				View.Home home = new View.Home();
				first.Show();
				//음... 안닫히네
				View.MainWindow mainWindow = new View.MainWindow();
				// 교수님 코드 VM.mainWindowRef.Close();
				first.pageControl.NavigationService.Navigate(home);
				*/



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
