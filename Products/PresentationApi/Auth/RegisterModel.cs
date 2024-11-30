using System.ComponentModel.DataAnnotations;

namespace WebApi.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }      
        public string? NameEn { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string? Mobile { get; set; }
        [Required(ErrorMessage = "ID Number is required")]
        public string? IDNumber { get; set; }
        public DateTime? Birthdate { get; set; }


    }
}
