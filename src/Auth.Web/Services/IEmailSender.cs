using System.Threading.Tasks;

namespace Auth.Web.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
