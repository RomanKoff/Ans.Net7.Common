﻿using MimeKit;

namespace Ans.Net7.Common
{

    public class MailMessage
	{
		public MailboxAddress From { get; set; }
		public MailboxAddress To { get; set; }
		public MailboxAddress[] Cc { get; set; }
		public MailboxAddress[] Bcc { get; set; }
		public string Subject { get; set; }
		public string ContentHtml { get; set; }
	}

}
