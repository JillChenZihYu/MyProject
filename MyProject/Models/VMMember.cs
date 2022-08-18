using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class VMMember
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請填寫Email做為您的帳號")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(30, ErrorMessage = "密碼最多30碼")]
        public string Password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請再填寫一次密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(30, ErrorMessage = "密碼最多30碼")]
        [Compare("Password", ErrorMessage = "兩次輸入不同")]
        public string ConfirmPassword { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "請填寫姓名")]
        [StringLength(30, ErrorMessage = "姓名不得超過30字")]
        public string Name { get; set; }

        [DisplayName("性別")]
        public bool Gender { get; set; }
       
        [DisplayName("生日")]
        [Required(ErrorMessage = "請填寫生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        [DisplayName("連絡電話")]
        [Required(ErrorMessage = "請輸入連絡電話")]
        [MaxLength(12, ErrorMessage = "不得超過12碼")]
        public string ContactNumber { get; set; }
        
        
    }
}