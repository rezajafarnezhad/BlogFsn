using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogFsn.Models.ViewInput
{
    public class RegisterViewInput
    {
        [Required]
        [DisplayName("نام و فامیلی")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("ایمیل")]

        public string Email { get; set; }

        [Required]
        [DisplayName("کلمه عبور")]

        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DisplayName("تکرار کلمه عبور")]

        public string ConfirmPassword { get; set; }

    }

    public class LoginViewInput
    {
        [Required]
        [DisplayName("ایمیل")]

        public string Email { get; set; }

        [Required]
        [DisplayName("کلمه عبور")]

        public string Password { get; set; }

        public bool RemmemberMe { get; set; }
    }
}
