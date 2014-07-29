using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class ChangeEmailFromKeyInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string Key { get; set; }
    }
}