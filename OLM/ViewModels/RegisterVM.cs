using System.ComponentModel.DataAnnotations;

namespace OLM.ViewModels
{
    public class RegisterVM
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }


    }
}
