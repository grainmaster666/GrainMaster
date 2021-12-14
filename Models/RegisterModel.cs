using System.ComponentModel.DataAnnotations;

namespace GrainMaster.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        [Required, Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class ChangePasswordModel
    {

        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword", ErrorMessage = "The NewPassword and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }


    }
}