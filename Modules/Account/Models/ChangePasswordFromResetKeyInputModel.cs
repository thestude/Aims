using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class ChangePasswordFromResetKeyInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password confirmation must match password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        [HiddenInput]
        public string Key { get; set; }
    }
}