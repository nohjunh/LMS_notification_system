using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Notice.ViewModel.Command.NoticeCommand
{
    class CheckBoxCommand : CheckBox
    {
        public static ViewModel VM = new ViewModel();



        public bool IsCheckBoxChecked
        {
            get { return (bool)GetValue(IsCheckBoxCheckedProperty); }
            set { SetValue(IsCheckBoxCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxCheckedProperty =
            DependencyProperty.Register("IsCheckBoxCheckedy", typeof(bool), typeof(CheckBoxCommand), new PropertyMetadata(OnCheckBoxCheckedChanged));

        private static void OnCheckBoxCheckedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxCommand checkBoxCommand = source as CheckBoxCommand;
            if(checkBoxCommand.IsCheckBoxChecked == true)
            {
                VM.check = true;
            }
            else
            {
                VM.check = false;
            }
        }








    }
}