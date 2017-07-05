using System.ComponentModel.DataAnnotations;

namespace Final.Models.Identity
{
    /*
     * ამ კლასში ვარჩევთ რა ტიპის ველები შუძლია ჰქონეს მომხმარებელს დარეგისტრირების მომენტში
     * იმ შემთხვევაში თუ გვსურს მომხმარებლისთვის რაიმე პრიორიტეტის მიცემა, მაშინ  შეიძლება ადმინს
     * ჰქონდეს მონაცემების შეცვლის შესაძლებლობა
     * */
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }
}
