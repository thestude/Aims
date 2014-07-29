using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class SendUsernameReminderInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}