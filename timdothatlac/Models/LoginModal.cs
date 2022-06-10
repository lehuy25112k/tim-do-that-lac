using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace timdothatlac.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string MailUser { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu ")]
        public string MatKhauUser { get; set; }

        //public bool RememberMe { get; set; }
    }
}