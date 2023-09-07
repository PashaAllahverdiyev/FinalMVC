using System.ComponentModel.DataAnnotations;

namespace Foxic.UI.ViewModels.AuthViewModels
{
	public class LoginVM
	{
		[Required,DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;
		[Required,DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		public bool RememberMe { get; set; }
	}
}
