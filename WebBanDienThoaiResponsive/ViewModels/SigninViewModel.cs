using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class SigninViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
    }
}