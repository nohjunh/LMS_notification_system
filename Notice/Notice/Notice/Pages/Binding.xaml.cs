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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notice.Pages
{
    /// <summary>
    /// Binding.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Binding : Page
    {
        public Binding()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender == oasis)
            {
                System.Diagnostics.Process.Start("https://all.jbnu.ac.kr/jbnu/oasis/index.html");
            }
            if(sender==newLMS)
            {
                System.Diagnostics.Process.Start("https://ieilms.jbnu.ac.kr/");
            }
            if(sender==oldLMS)
            {
                System.Diagnostics.Process.Start("https://ieilmsold.jbnu.ac.kr/login.php?errorcode=3");
            }
        }
    }
}
