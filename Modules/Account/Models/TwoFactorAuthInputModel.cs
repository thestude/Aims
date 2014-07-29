using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class TwoFactorAuthInputModel
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}