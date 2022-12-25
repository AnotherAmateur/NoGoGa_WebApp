using System.ComponentModel.DataAnnotations;

namespace NoGoGa.Models
{
	public class VMLogin
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string PassWord { get; set; }
		public bool KeepLoggedIn { get; set; }
	}
}
