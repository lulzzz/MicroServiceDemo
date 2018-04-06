using System.ComponentModel.DataAnnotations;

namespace DNIC.AccountCenter.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
