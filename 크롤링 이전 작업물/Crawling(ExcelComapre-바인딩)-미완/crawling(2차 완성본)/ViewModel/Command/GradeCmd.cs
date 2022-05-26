using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace crawling.ViewModel.Command
{
    class GradeCmd : ICommand
    {
        Action<object> executemethod;
        Func<object, bool> canexcutemethod;

        public GradeCmd(Action<object> e, Func<object, bool> f)
        {
            executemethod = e;
            canexcutemethod = f;
        }

        public event EventHandler CanExecuteChanged
        {
            add{ CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canexcutemethod != null)
                return canexcutemethod(parameter);
            else
                return false;
        }

        public void Execute(object parameter)
        {
            Execute(parameter);
        }
    }
}
