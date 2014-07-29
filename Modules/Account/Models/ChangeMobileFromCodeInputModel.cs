using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class ChangeMobileFromCodeInputModel
    {
        [Required]
        public string Code { get; set; }
    }
    
}