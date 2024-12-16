using System.ComponentModel.DataAnnotations;

namespace Meetings_App.Models.DTO
{
    public class RegisterRequestDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
