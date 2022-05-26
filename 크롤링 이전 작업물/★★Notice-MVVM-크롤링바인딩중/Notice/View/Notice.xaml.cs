using System;
using System.Linq;
using System.Windows.Controls;

namespace Notice.View
{
    /// <summary>
    /// Notice.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Notice : Page
    {
		public ViewModel.ViewModel VM { get; set; }
		public Notice(ViewModel.ViewModel vm)
		{
			VM = vm;
		}

		//int grade = 0;
		public Notice()
        {
            InitializeComponent();
			//Searching.Click += button1_Initialized;

		}


		public void button1_Initialized(object sender, EventArgs e)
		{
			foreach (CheckBox cbx in stp.Children.OfType<CheckBox>())
			{

				if (cbx.Content.ToString() == "21.5학점(대진설O)")
				{
					//grade = 21;
					//VM.grade.GradeNumber = 21;
				}
				if (cbx.Content.ToString() == "18.5학점(대진설O)")
				{
					//VM.grade.GradeNumber = 18;
				}
				if (cbx.Content.ToString() == "15.5학점(대진설O)")
				{
					//VM.grade.GradeNumber = 15;
				}
			}
		}

    }
}
