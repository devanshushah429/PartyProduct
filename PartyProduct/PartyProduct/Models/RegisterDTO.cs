using System.ComponentModel.DataAnnotations;

namespace PartyProduct.Models
{
    public class RegisterDTO
    {
        [Required]
        public string PersonName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$",ErrorMessage="Phone number should contain only numbers")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
