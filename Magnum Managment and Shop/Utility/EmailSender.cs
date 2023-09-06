using Microsoft.AspNetCore.Identity.UI.Services;

namespace Magnum_Managment_and_Shop.Utility
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			return Task.CompletedTask;
		}
	}
}
