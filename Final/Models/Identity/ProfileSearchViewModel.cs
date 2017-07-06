using System.ComponentModel.DataAnnotations;

namespace Final.Models.Identity
{
    public class ProfileSearchViewModel
    {

        [Display(Name="Enter search object email: ")]
        public string Email { get; set; }
    }
}
