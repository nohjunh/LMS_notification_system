using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using crawling.Model;
using crawling.ViewModel.Command;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



namespace crawling.ViewModel
{
    public class CrawlingVM
    {

        public ObservableCollection<LmsData1> L_Data1_Main { get; set; }
        public ObservableCollection<LmsData2> L_Data2_Main { get; set; }
        public ObservableCollection<LmsData3> L_Data3_Main { get; set; }
        public ObservableCollection<DepartmentData> D_Data_Main { get; set; }
        public List<LmsData1> L_Data1 { get; set; }
        public List<LmsData2> L_Data2 { get; set; }
        public List<LmsData3> L_Data3 { get; set; }
        public List<DepartmentData> D_Data { get; set; }

        
        public LoginLmsCmd LoginLmsCmd { get; set; }
        public DataLmsCmd DataLmsCmd { get; set; }
        public LoginModel LoginModel { get; set; }
        public ReportLmsCmd ReportLmsCmd { get; set; }
        public NoticeLmsCmd NoticeLmsCmd { get; set; }

        public DepartmentDataCmd DepartmentDataCmd { get; set; }


        public CrawlingVM()
        {


            L_Data1_Main = new ObservableCollection<LmsData1>();
            L_Data2_Main = new ObservableCollection<LmsData2>();
            L_Data3_Main = new ObservableCollection<LmsData3>();
            D_Data_Main = new ObservableCollection<DepartmentData>();

            L_Data1 = new List<LmsData1>();
            L_Data2 = new List<LmsData2>();
            L_Data3 = new List<LmsData3>();
            D_Data = new List<DepartmentData>();

            DepartmentDataCmd = new DepartmentDataCmd(this);
            LoginModel = new LoginModel();
            LoginLmsCmd = new LoginLmsCmd(this);
            DataLmsCmd = new DataLmsCmd(this);
            ReportLmsCmd = new ReportLmsCmd(this);
            NoticeLmsCmd = new NoticeLmsCmd(this);
        }

        public void get1()
        {
            for(int i=0; i<L_Data1.Count(); i++)
            {
                L_Data1_Main.Add(L_Data1[i]);
            }
        }

        public void get2()
        {
            for (int i = 0; i < L_Data2.Count(); i++)
            {
                L_Data2_Main.Add(L_Data2[i]);
            }
        }

        public void get3()
        {
            for (int i = 0; i < L_Data3.Count(); i++)
            {
                L_Data3_Main.Add(L_Data3[i]);
            }
        }
        public void get4()
        {
            for (int i = 0; i < D_Data.Count(); i++)
            {
                D_Data_Main.Add(D_Data[i]);
            }
        }
    }
}
