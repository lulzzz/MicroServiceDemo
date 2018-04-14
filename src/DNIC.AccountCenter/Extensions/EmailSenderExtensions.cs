using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DNIC.AccountCenter.Services;

namespace DNIC.AccountCenter.Extensions
{
	public static class EmailSenderExtensions
	{
		public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
		{
			await Task.Factory.StartNew(() => { });
			//return emailSender.SendEmail(email, "Confirm your email",
			//    $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
		}
	}
}
