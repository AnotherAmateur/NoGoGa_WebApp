using System.ComponentModel.DataAnnotations;

namespace NoGoGa.Models
{
	public class VMRegistration
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string PassWord { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string FullName { get; set; }
		public bool KeepLoggedIn { get; set; }
	}
}