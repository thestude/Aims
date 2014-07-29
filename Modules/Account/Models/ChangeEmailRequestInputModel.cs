using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class ChangeEmailRequestInputModel
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}