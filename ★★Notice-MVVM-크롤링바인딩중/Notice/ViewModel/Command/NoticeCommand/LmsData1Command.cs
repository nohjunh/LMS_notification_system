﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notice.Model;
using System.Windows;
//Selenium 
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Windows.Input;

namespace Notice.ViewModel.Command
{
    public class LmsData1Command : ICommand
    {
		int countBtn2 = 0;
		int grade = 21; //Test

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		public ViewModel VM { get; set; }
		public LmsData1Command(ViewModel vm)
		{
			VM = vm;
		}

		//ICommand 인터페이스 구현
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (countBtn2 != 0)
			{
				VM.L_Data1.Clear();
			}
			Start2();
			countBtn2++;
		}

		//메소드
		private async void Start2()
		{
			var task2 = Task.Run(() => DataCrawling());
			await task2;
			VM.get1();
		}

		public void DataCrawling()
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
				element.SendKeys(VM.mainWindowIDInformation.LoginID);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(VM.mainWindowIDInformation.LoginPasswd);

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

			element = _driver.FindElementByXPath("//*[@id='nav']/li[3]/a");
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
					TextUpLoad2();
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
					TextUpLoad2();
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
					TextUpLoad2();
				}
			}
			_driver.Close();
		}

		public void TextUpLoad2()
		{
			if (_driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr").Text == "해당하는 자료 정보가 없습니다.")
			{
				VM.L_Data1.Add(new LmsData1()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = "업로드된 자료가 없습니다."
				});
				return;
			}
			else
			{
				VM.L_Data1.Add(new LmsData1()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[2]").Text,
					LmsRdate = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[4]").Text,

				});
			}
		}

		public void saveExcel()
        {

        }
	}
}
