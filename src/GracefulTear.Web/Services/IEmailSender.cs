using System.Threading.Tasks;

namespace GracefulTear.Web.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
