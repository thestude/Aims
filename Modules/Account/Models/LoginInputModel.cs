﻿using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Models
{
    public class LoginInputModel
    {
        [Required]
        [Display(Name="Username or Email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [ScaffoldColumn(false)]
        public string ReturnUrl { get; set; }
    }
}