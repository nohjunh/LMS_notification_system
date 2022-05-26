using Notice.Classes;
using Notice.Model;
using Notice.ViewModel.Command;
using Notice.ViewModel.Command.FirstPageCommand;
using Notice.ViewModel.Command.HomePageCommand;
using Notice.ViewModel.Command.LoginPageCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.ViewModel
{
    public class ViewModel
    {
        /// <summary>
        /// HomePageCommand
        /// </summary>
        public List<DepartmentData> D_Data { get; set; } 
        public DepartmentDataButtonCommand departmentDataButtonCommand { get; set; }

        /// <summary>
        /// LoginPageCommand
        /// </summary>
        public MainWindowIDInformation mainWindowIDInformation { get; set; }
        public exitButtonCommand exitButtonCommand { get; set; }
        public signinButtonCommand loginButtonCommand { get; set; }

        /// <summary>
        /// FirstPageCommnad
        /// </summary>
        public listViewSelected listViewSelected { get; set; }

        /// <summary>
        /// BindingPageCommand
        /// </summary>
        public BindingButton bindingButton { get; set; }
        public ViewModel()
        {

            D_Data = new List<DepartmentData>();

            mainWindowIDInformation = new MainWindowIDInformation();
            exitButtonCommand = new exitButtonCommand();
            loginButtonCommand = new signinButtonCommand();
            bindingButton = new BindingButton();
           
            departmentDataButtonCommand = new DepartmentDataButtonCommand(this);
            listViewSelected = new listViewSelected();


        }


    }
}
