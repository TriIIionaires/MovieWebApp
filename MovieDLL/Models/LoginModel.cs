using System.ComponentModel.DataAnnotations;

namespace MovieDLL.Models
{
	public class LoginModel
	{
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username cannot be empty.")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty.")]
        public string? Password { get; set; }
    }
}
