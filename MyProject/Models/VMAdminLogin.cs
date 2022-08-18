using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class VMAdminLogin
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請填寫Email作為您的帳號")]
        [EmailAddress(ErrorMessage = "Email格式有誤")]
        public string Email { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}