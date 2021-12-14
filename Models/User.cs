using System.ComponentModel.DataAnnotations;

namespace GrainMaster.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}