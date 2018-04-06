using System.ComponentModel.DataAnnotations;

namespace DNIC.AccountCenter.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
