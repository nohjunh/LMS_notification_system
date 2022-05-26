
using Newtonsoft.Json.Linq;
using Notice.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Notice.ViewModel.NaverMail
{
    public class NaverMailManager
	{
		public ViewModel VM { get; set; }

		private string content;
		public NaverMailManager()
		{

		}

		public void sendMail(string c)
		{
			content = c;
			try
			{
				MailMessage mailMessage = new MailMessage();

				mailMessage.From = new MailAddress("byuntil20@gmail.com", "알림이", System.Text.Encoding.UTF8);
				// 받는이 메일 주소
				mailMessage.To.Add(NaverData.userEmail);
				// 참조 메일 주소
				mailMessage.CC.Add(NaverData.userEmail);
				// 비공개 참조 메일 주소
				mailMessage.Bcc.Add(NaverData.userEmail);
				// 제목
				mailMessage.Subject = "강의자료 OR 레포트 업데이트";
				// 메일 제목 인코딩 타입(UTF-8) 선택
				mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
				// 본문
				mailMessage.Body = "<html><body><h2>" + content + "</body></html>";
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
