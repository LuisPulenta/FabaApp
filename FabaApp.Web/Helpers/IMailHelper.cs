using FabaApp.Common.Models;

namespace FabaApp.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}