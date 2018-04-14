using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
