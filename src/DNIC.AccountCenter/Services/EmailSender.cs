using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Services
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Task.CompletedTask;
		}
	}
}
