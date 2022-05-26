using System;
using System.Net.Mail;
using System.Windows;

namespace 메일_보내기_체험_API_사용안하고_
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MailMessage mailMessage = new MailMessage();

				mailMessage.From = new MailAddress("byuntil20@gmail.com", "성재", System.Text.Encoding.UTF8);
				// 받는이 메일 주소
				mailMessage.To.Add("gugugu2014@naver.com");
				// 참조 메일 주소
				mailMessage.CC.Add("gugugu2014@naver.com");
				// 비공개 참조 메일 주소
				mailMessage.Bcc.Add("gugugu2014@naver.com");
				// 제목
				mailMessage.Subject = "안뇽하세요";
				// 메일 제목 인코딩 타입(UTF-8) 선택
				mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
				// 본문
				mailMessage.Body = "<html><body><h2>여기에 본문을 입력해보세요</h2><h3>html로도 이렇게 보낼수 있어요</h3></body></html>";
				// 본문의 포맷에 따라 선택
				mailMessage.IsBodyHtml = true;
				// 본문 인코딩 타입(UTF-8) 선택
				mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
				// 파일 첨부
				//mailMessage.Attachments.Add(new Attachment(new FileStream(@"D:\test.zip", FileMode.Open, FileAccess.Read), "test.zip"));
				// SMTP 서버 주소
				SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
				// SMTP 포트
				SmtpServer.Port = 587;
				// SSL 사용 여부
				SmtpServer.EnableSsl = true;
				SmtpServer.UseDefaultCredentials = false;
				SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				SmtpServer.Credentials = new System.Net.NetworkCredential("byuntil20", "dkssudgktpdyrntjdwodlqslek");

				SmtpServer.Send(mailMessage);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}
