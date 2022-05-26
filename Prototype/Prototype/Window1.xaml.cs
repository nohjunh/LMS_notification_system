using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prototype
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility / Enter를 누르면 tooltip이 보이게 되는 기능
            if(nav_panel_toggleButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_calendar.Visibility = Visibility.Collapsed;
                tt_alert.Visibility = Visibility.Collapsed;
                tt_link.Visibility = Visibility.Collapsed;
                tt_setting.Visibility = Visibility.Collapsed;
                tt_signOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_calendar.Visibility = Visibility.Visible;
                tt_alert.Visibility = Visibility.Visible;
                tt_link.Visibility = Visibility.Visible;
                tt_setting.Visibility = Visibility.Visible;
                tt_signOut.Visibility = Visibility.Visible;
            }
        }
    }
}
