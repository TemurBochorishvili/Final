using System.ComponentModel.DataAnnotations;

namespace Final.Models.Identity
{
    public class LoginViewModel
    {
        
        //public string UserName { get; set; } ბაზაში არის username

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
