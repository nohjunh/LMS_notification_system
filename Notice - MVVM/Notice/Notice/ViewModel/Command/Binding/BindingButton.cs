using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notice.ViewModel.Command
{
    public class BindingButton : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string newOlblmsOrOasis = parameter.ToString();
            if (newOlblmsOrOasis == "oasis")
            {
                System.Diagnostics.Process.Start("https://all.jbnu.ac.kr/jbnu/oasis/index.html");
            }
            if (newOlblmsOrOasis == "newLMS")
            {
                System.Diagnostics.Process.Start("https://ieilms.jbnu.ac.kr/");
            }
            if (newOlblmsOrOasis == "oldLMS")
            {
                System.Diagnostics.Process.Start("https://ieilmsold.jbnu.ac.kr/login.php?errorcode=3");
            }
        }
    }
}
