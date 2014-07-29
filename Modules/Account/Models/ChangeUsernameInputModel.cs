using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class ChangeUsernameInputModel
    {
        [Required]
        public string NewUsername { get; set; }
    }
}