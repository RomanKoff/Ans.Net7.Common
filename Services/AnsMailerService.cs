using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace Ans.Net7.Common.Services
{

    public interface IMailerService
	{
		Task SendAsync(MailMessage message);
	}



	public interface IMailerServiceOptions
	{
		string SmtpServer { get; }
		int SmtpPort { get; }
		bool SmtpUseSsl { get; }
		string SmtpUsername { get; }
		string SmtpPassword { get; }
		string DefaultFromAddress { get; }
		string DefaultFromTitle { get; }
		string DebugCc { get; }
	}



	public class AnsMailerService
		: IMailerService
	{

		private readonly IMailerServiceOptions _options;


		/* ctors */


		public AnsMailerService(
			IMailerServiceOptions options)
		{
			_options = options;
		}


		/* functions */


		public static MailboxAddress GetMailboxAddress(
			string title,
			string address)
		{
			return new MailboxAddress(Encoding.UTF8, title, address);
		}


		public static MailboxAddress GetMailboxAddress(
			string address)
		{
			return GetMailboxAddress(address, address);
		}


		public async Task SendAsync(
			MailMessage message)
		{
			var m1 = new MimeMessage();
			m1.From.Add(message.From ?? GetMailboxAddress(
				_options.DefaultFromTitle, _options.DefaultFromAddress));
			m1.To.Add(message.To);
			if (message.Cc != null && message.Cc.Any())
				m1.Cc.AddRange(message.Cc);
			if (message.Bcc != null && message.Bcc.Any())
				m1.Bcc.AddRange(message.Bcc);
			m1.Subject = message.Subject;
			m1.Body = new TextPart(TextFormat.Html)
			{
				Text = message.ContentHtml
			};
			using var client1 = new SmtpClient();
			client1.AuthenticationMechanisms.Remove("XOAUTH2");
			await client1.ConnectAsync(
				_options.SmtpServer,
				_options.SmtpPort,
				_options.SmtpUseSsl);
			await client1.AuthenticateAsync(
				_options.SmtpUsername,
				_options.SmtpPassword);
			await client1.SendAsync(m1);
			await client1.DisconnectAsync(true);
		}

	}

}
